namespace DeliverySoft.APIGateway.Helpers;

public static class XmlCommentHelpers
{
    
    public static void FillFilesByPattern(string directory, List<string> xmlCommentFiles, string pattern)
    {
        if (!Directory.Exists(directory))
        {
            return;
        }
                
        xmlCommentFiles.AddRange(Directory.GetFiles(directory, pattern));

        foreach (var currentDirectory in Directory.GetDirectories(directory))
        {
            FillFilesByPattern(currentDirectory, xmlCommentFiles, pattern);
        }
    }
}