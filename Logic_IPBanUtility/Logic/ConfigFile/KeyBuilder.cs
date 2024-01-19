using Logic_IPBanUtility.Models;

namespace Logic_IPBanUtility;

public class KeyBuilder
{
    public List<string> Context;

    public KeyBuilder(List<string> context)
    {
        Context = context;
    }

    public List<Key> GetKeys(List<KeyIdenti> keyIdentis)
    {
        List<Key> newKeys = new();
        foreach (var keyIdenti in keyIdentis)
            newKeys.Add(GetKey(keyIdenti));
        return newKeys;
    }
    private Key GetKey(KeyIdenti keyIdenti)
    {
        var keyContext = GetKeyContext(keyIdenti.Name);
        var index = Context.IndexOf(keyContext);
        var comment = GetKeyComment(index);
        Key key = new(index, keyContext, comment, keyIdenti);
        return key;
    }
    private string GetKeyContext(string name)
    {
        var keyContext = Context.FirstOrDefault(x => x.Contains($"add key=\"{name}\""));
        if (keyContext is null)
            throw new KeyNotFoundException($"Не знайдено контекст для ключа: {name} " +
                 $"\n Перевірте наявність ключа в файлі конфігурації IPBan та в списку ключів KeyIdentis");
        return keyContext;
    }

    #region GetKeyComment
    private string GetKeyComment(int index)
    {
        List<string> comment = new();
        for (int i = index; ;)
        {
            var str = Context[--i];
            str = RemoveEmptyLine(str);
            str = RemoveDoubleSpaces(str);
            if (str.Contains("\n<!--"))
                break;
            else if (str.Contains("<!--"))
            {
                comment.Add(str);
                break;
            }
            else
                comment.Add(str);
        }
        comment.Reverse();
        var result = string.Join(' ', comment);
        return result;
    }
    private string RemoveDoubleSpaces(string input)
    {
        while (input.Contains("\t"))
            input = input.Replace("\t", "\n");
        return input;
    }
    private string RemoveEmptyLine(string input)
    {
        while (input.Contains("\n"))
            input = input.Replace("\n", "");
        return input;
    }
    #endregion
}
