using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class FbxAnimListPostprocessor : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        if (Path.GetExtension(assetPath).ToLower() == ".fbx"
            && !assetPath.Contains("@"))
        {
            try
            {
                // Remove 6 chars because dataPath and assetPath both contain "assets" directory  
                string fileAnim = Application.dataPath + Path.ChangeExtension(assetPath, ".txt").Substring(6);
                StreamReader file = new StreamReader(fileAnim);

                string sAnimList = file.ReadToEnd();
                file.Close();

                if (EditorUtility.DisplayDialog("FBX Animation Import from file",
                    fileAnim, "Import", "Cancel"))
                {
                    System.Collections.ArrayList List = new ArrayList();
                    ParseAnimFile(sAnimList, ref List);

                    ModelImporter modelImporter = assetImporter as ModelImporter;
                    //modelImporter.splitAnimations = true;  
                    modelImporter.clipAnimations = (ModelImporterClipAnimation[])
                        List.ToArray(typeof(ModelImporterClipAnimation));

                    EditorUtility.DisplayDialog("Imported animations",
                        "Number of imported clips: "
                        + modelImporter.clipAnimations.GetLength(0).ToString(), "OK");
                }
            }
            catch { }
            // (Exception e) { EditorUtility.DisplayDialog("Imported animations", e.Message, "OK"); }  
        }
    }

    void ParseAnimFile(string sAnimList, ref System.Collections.ArrayList List)
    {
        Regex regexString = new Regex(" *(?<firstFrame>[0-9]+) *- *(?<lastFrame>[0-9]+) *(?<loop>(loop|noloop|clampF|once| )) *(?<name>[^\r^\n]*[^\r^\n^ ])",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        Match match = regexString.Match(sAnimList, 0);
        while (match.Success)
        {
            ModelImporterClipAnimation clip = new ModelImporterClipAnimation();

            if (match.Groups["firstFrame"].Success)
            {
                clip.firstFrame = System.Convert.ToInt32(match.Groups["firstFrame"].Value, 10);
            }
            if (match.Groups["lastFrame"].Success)
            {
                clip.lastFrame = System.Convert.ToInt32(match.Groups["lastFrame"].Value, 10);
            }
            if (match.Groups["loop"].Success)
            {
                clip.loop = match.Groups["loop"].Value == "loop";

                if (match.Groups["loop"].Value == "loop")
                {
                    clip.wrapMode = WrapMode.Loop;
                    //clip.loop = true;
                    clip.loopTime = true;
                }
                else if (match.Groups["loop"].Value == "clampF")
                {
                    clip.wrapMode = WrapMode.ClampForever;
                }
                else if (match.Groups["loop"].Value == "once")
                {
                    clip.wrapMode = WrapMode.Once;
                }
                else
                {
                    clip.wrapMode = WrapMode.Default;
                }
            }
            if (match.Groups["name"].Success)
            {
                clip.name = match.Groups["name"].Value;
            }

            List.Add(clip);

            match = regexString.Match(sAnimList, match.Index + match.Length);
        }
    }
}