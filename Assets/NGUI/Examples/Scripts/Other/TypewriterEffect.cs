using UnityEngine;

/// <summary>
/// Trivial script that fills the label's contents gradually, as if someone was typing.
/// </summary>

[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	public int charsPerSecond = 40;

	UILabel mLabel;
	string mText;
	int mOffset = 0;
	float mNextChar = 0f;
    bool mFinished = true;

    public bool IsFinished
    {
        get { return mFinished; }
    }

    void Awake()
    {
        mLabel = GetComponent<UILabel>();
        mLabel.supportEncoding = false;
        mLabel.symbolStyle = UIFont.SymbolStyle.None;
        Vector2 scale = mLabel.cachedTransform.localScale;
        mLabel.font.WrapText(mLabel.text, out mText, mLabel.lineWidth / scale.x, mLabel.lineHeight / scale.y, mLabel.maxLineCount, false, UIFont.SymbolStyle.None);
    }

	void Update ()
	{
		if (mOffset < mText.Length)
		{
			if (mNextChar <= Time.time)
			{
				charsPerSecond = Mathf.Max(1, charsPerSecond);

				// Periods and end-of-line characters should pause for a longer time.
				float delay = 1f / charsPerSecond;
				char c = mText[mOffset];
				if (c == '.' || c == '\n' || c == '!' || c == '?') delay *= 4f;

				mNextChar = Time.time + delay;
				mLabel.text = mText.Substring(0, ++mOffset);
			}
            mFinished = false;
		}
        else mFinished = true;
	}

    public void Reset()
    {
        mOffset = 0;
        mNextChar = 0f;
        mText = mLabel.text;
        mFinished = true;
    }
}
