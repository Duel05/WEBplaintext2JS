string book_name = args[0];

string[] files = Directory.GetFiles("web/");
List<List<string>> chapters = new List<List<string>>();
foreach (string file in files)
{
    List<string> chapter = new List<string>();
    
    string contents = File.ReadAllText(file);
    string[] lines = contents.Split(
        new string[] { "\r\n", "\r", "\n" },
        StringSplitOptions.None
    );
    
    for (int i = 0; i < lines.Length; i++)
    {
        if (i > 1)
        {
            chapter.Add(lines[i]);
        }
    }

    chapters.Add(chapter);
}

string js = "const " + book_name + " = [";
List<string> last = chapters.Last();
foreach (List<string> chapter in chapters)
{
    js += "{";
    js += "verses: [";
    int i = -1;
    string v_last = chapter.Last();
    foreach (string verse in chapter)
    {
        js += "`" + verse + "`";
        if (!verse.Equals(v_last)) js += ",";
    }
    js += "]";
    js += "}";
    if (!chapter.Equals(last)) js += ",";
}
js += "];";
File.WriteAllText("output.txt", js);