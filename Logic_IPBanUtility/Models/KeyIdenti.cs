namespace Logic_IPBanUtility.Models;

public class KeyIdenti
{
     public bool IsHidden { get; set; }
     public string Name { get; set; }
     public KeyIdenti(bool isHidden, string name)
     {
          IsHidden = isHidden;
          Name = name;
     }
}