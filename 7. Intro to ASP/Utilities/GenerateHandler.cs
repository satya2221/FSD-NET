namespace _7._Intro_to_ASP;

public class GenerateHandler
{
    public static string Nik(string? nik)
    {
        if(nik == null)
        {
            return "111111";
        }
        var convertToInt = Convert.ToInt32(nik);
        return Convert.ToString(convertToInt +1);
    }
}
