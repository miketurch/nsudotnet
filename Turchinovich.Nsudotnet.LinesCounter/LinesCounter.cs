using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Turchinovich.Nsudotnet.LinesCounter
{
    class LinesCounter
    {
        static void Main(string[] args)
        {
	        Console.WriteLine(CountNumberOfLines(args[0]));
        }

	    public static int CountNumberOfLines(String typeOfFiles)
	    {
			FileInfo[] files = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*." + typeOfFiles, SearchOption.AllDirectories);
		    int totalNumberOfLines = 0;
			Parallel.ForEach(files, currentFile =>
		    {
				Interlocked.Add(ref totalNumberOfLines, CountNumberOfLine(currentFile));
		    });
		    return totalNumberOfLines;
	    }

	    private static int CountNumberOfLine(Object file)
	    {
			FileInfo currentFile = (FileInfo)file;
		    int count = 0;
		    int inComment = 0;
			using (StreamReader sr = currentFile.OpenText())
		    {
			    string line;
			    while ((line = sr.ReadLine()) != null)
			    {
				    if (IsRealCode(line.Trim(), ref inComment))
				    {
					    count++;
				    }
			    }
		    }
		    return count;
	    }

	    private static bool IsRealCode(string trimmed, ref int inComment)
	    {
		    if (trimmed.StartsWith("/*") && trimmed.EndsWith("*/"))
		    {
			    return false;
		    }
		    if (trimmed.StartsWith("/*"))
		    {
			    inComment++;
			    return false;
		    }
		    if (trimmed.EndsWith("*/"))
		    {
			    inComment--;
			    return false;
		    }
		    return inComment == 0 && !trimmed.StartsWith("//");
	    }
    }
}
