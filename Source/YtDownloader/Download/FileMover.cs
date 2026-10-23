namespace YtDownloader.Download;

public sealed class FileMover
{
    private readonly string destination;

    public FileMover(string destination)
    {
        this.destination = destination;
    }

    public void Move(string folder)
    {
        Directory.CreateDirectory(destination);

        foreach (string file in Directory.GetFiles(folder))
            MoveFile(file);

        Directory.Delete(folder, true);
    }

    private void MoveFile(string file)
    {
        string target = Path.Combine(destination, Path.GetFileName(file));
        Console.WriteLine($"Moving: {file} -> {target}");
        File.Move(file, target, true);
    }
}
