using GI.Common.XF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GI.Common.XF.Helpers
{
    public class AudioHelper
    {
        public async Task GetAudioText()
        {
            await DependencyService.Get<ISpeechToText>().SpeechToText();
        }
    }
}
