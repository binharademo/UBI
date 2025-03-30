using System.Linq;
using System.Threading.Tasks;
using Xamanimation;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using UXDivers.Grial;

namespace Ubi
{
    [ContentProperty(nameof(Animations))]
    public class AnimationWrapper : ITriggerAction
    {
        public AnimationWrapper()
        {
            Animations = new Collection<AnimationBase>();
        }

        public Collection<AnimationBase> Animations { get; set; }

        public Task Execute()
        {
            return Task.WhenAll(Animations.Select(x => x.Begin()));
        }
    }
}
