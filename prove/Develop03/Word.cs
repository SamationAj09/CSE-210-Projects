public class Word {
    private string _text;
    private bool _isHidden;

    public Word(string text) => _text = text;

    public void Hide() => _isHidden = true;

    public bool IsHidden() => _isHidden;

    public string GetDisplayText() => _isHidden ? new string('_', _text.Length) : _text;
}