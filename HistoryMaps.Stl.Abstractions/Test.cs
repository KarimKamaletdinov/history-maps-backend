using IxMilia.Stl;

namespace HistoryMaps;

public class Repository
{
	public void W()
	{
		StlFile stlFile;
		using (FileStream fs = new FileStream(@"C:\Path\To\File.stl", FileMode.Open))
		{
			stlFile = StlFile.Load(fs);
		}
	}
}