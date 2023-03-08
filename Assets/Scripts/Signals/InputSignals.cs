using Extensions;
using Keys;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class InputSignals:MonoSingleton<InputSignals>
    {
        public UnityAction onEnableInput = delegate {  };
        public UnityAction onDisableInput = delegate {  };
        public UnityAction onFirstTimeTouchTaken = delegate {  };
        public UnityAction onInputTaken = delegate {  };
        public UnityAction onInputReleased = delegate {  };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate {  };
    }
}