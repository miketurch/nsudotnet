using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
				Interlocked.Add(ref totalNumberOfLines, CountNumberOfLine(currentFile).Result);
			});
		    return totalNumberOfLines;
	    }

	    private static async Task<int> CountNumberOfLine(Object file)
	    {
			FileInfo currentFile = (FileInfo)file;
		    int count = 0;

		    bool comment = false;
		    bool multicomment = false;
		    bool realCode = false;

		    int currentSymbol = 0;

		    using (StreamReader sr = currentFile.OpenText())
		    {
			    char[] buffer = new char[1];
				while (await sr.ReadAsync(buffer, 0, 1) != 0)
				{
					int readedSymbol = buffer[0];
					var lastSymbol = currentSymbol;
					currentSymbol = readedSymbol;
					if (currentSymbol.Equals(' '))
					{
						continue;
					}
					if (currentSymbol.Equals('/') && lastSymbol.Equals('/') )
					{
						comment = true;
						continue;
					}

					if (currentSymbol.Equals('\n') && lastSymbol.Equals('\r'))
					{
						if (realCode)
						{
							realCode = false;
							count++;
						}

						comment = false;
						continue;
					}
					if (currentSymbol.Equals('*') && lastSymbol.Equals('/'))
					{
						multicomment = true;
						continue;
					}
					if (currentSymbol.Equals('/') && lastSymbol.Equals('*'))
					{
						multicomment = false;
						continue;
					}
					if (currentSymbol.Equals('/'))
					{
						continue;
					}
					if (currentSymbol.Equals('\r'))
					{
						continue;
					}
					if (!comment && !multicomment)
					{
						realCode = true;
					}
				}
			    if (realCode)
			    {
				    count++;
			    }
		    }
		    return count;
	    }
    }
}
