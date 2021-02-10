using SNS.Library;
using System.Threading.Tasks;

namespace SNS
{
    class Program
    {
        static async Task Main(string[] _)
        {
            await new Startup().Run();
        }
    }
}
