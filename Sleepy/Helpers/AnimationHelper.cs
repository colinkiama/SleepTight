using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Sleepy.Helpers
{
    public class AnimationHelper
    {
        public event EventHandler ScaleAnimCompleted;

        public void PrepForScaleAnim(Frame frameToScaleDown)
        {
            frameToScaleDown.Scale(0.1f, 0.1f,duration:0).Fade(0).Start();
        }

        public async Task ScaleAnimation(Frame frameToScale)
        {
            await frameToScale.Scale(1f, 1f).Fade(1,0).StartAsync();
            ScaleAnimCompleted?.Invoke(this, EventArgs.Empty);
        }

        public async Task ScaleDownAnimation(Frame frameToScale)
        {
            await frameToScale.Scale(0.1f, 0.1f).Fade(0).StartAsync();
        }
    }
}
