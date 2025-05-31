public class Reference {
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;
    private bool _hasRange;

    public Reference(string book, int chapter, int verse) : this(book, chapter, verse, 0) {
        _hasRange = false;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse) {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
        _hasRange = true;
    }

    public string GetDisplayText() => _hasRange ? $"{_book} {_chapter}:{_startVerse}-{_endVerse}" : $"{_book} {_chapter}:{_startVerse}";
}