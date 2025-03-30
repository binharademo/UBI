using System;
using System.Threading.Tasks;
using Xamanimation;
using UXDivers.Grial;

namespace Ubi
{
    public class TranslateYAnimation : TranslateToAnimation, ITriggerAction
    {
        public Task Execute()
        {
            return BeginAnimation();
        }

        protected override Task BeginAnimation()
        {
            TranslateX = Target.TranslationX;

            return base.BeginAnimation();
        }
    }
}
