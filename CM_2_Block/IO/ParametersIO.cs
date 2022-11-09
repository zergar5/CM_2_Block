namespace CM_2_Block.IO;

public class ParametersIO
{
    private readonly string _path;

    public ParametersIO(string path)
    {
        _path = path;
    }

    public (double, double, int, int) ReadMethodParameters(string fileName)
    {
        using var streamReader = new StreamReader(_path + fileName);
        var paramsIn = streamReader.ReadLine().Replace('.', ',').Split(' ');
        var parameters = (double.Parse(paramsIn[0]), double.Parse(paramsIn[1]), int.Parse(paramsIn[2]), int.Parse(paramsIn[3]));
        return parameters;
    }
}