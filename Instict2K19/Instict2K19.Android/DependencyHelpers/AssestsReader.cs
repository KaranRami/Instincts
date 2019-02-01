using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content.Res;
using Instict2K19.Droid.DependencyHelpers;
using Instict2K19.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(AssestsReader))]
namespace Instict2K19.Droid.DependencyHelpers
{
    public class AssestsReader: IAssestsReader
    {
        public async Task<string> ReadAssets(string filename)
        {
            string content = string.Empty;
            await Task.Run(() => {
                using (AssetManager assets = Android.App.Application.Context.Assets)
                {
                    using (StreamReader sr = new StreamReader(assets.Open(filename)))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            });
            return content;
        }
    }
}
