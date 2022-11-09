using System.Globalization;

namespace CM_2_Block.IO;

public class VectorIO
{
    private static readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("en-US");
    private readonly string _path;

    public VectorIO(string path)
    {
        _path = path;
    }

    public double[] ReadDouble(string fileName)
    {
        using var streamReader = new StreamReader(_path + fileName);
        var text = streamReader.ReadToEnd().Replace('.', ',');
        var vector = text.Split(' ').Select(double.Parse).ToArray();
        return vector;
    }

    public void Write(double[] vector, string fileName)
    {
        using var streamWriter = new StreamWriter(_path + fileName);
        foreach (var element in vector)
        {
            streamWriter.WriteLine(element.ToString("0.00000000000000e+00", _culture));
        }
    }

}