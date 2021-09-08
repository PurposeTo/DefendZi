using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public class AnimatorParameters
    {
        private readonly Animator _unityAnimator;
        private readonly AnimatorControllerParameter[] _parameters;
        private readonly Dictionary<string, AnimatorControllerParameter> _floats;
        private readonly Dictionary<string, AnimatorControllerParameter> _ints;
        private readonly Dictionary<string, AnimatorControllerParameter> _bools;
        private readonly Dictionary<string, AnimatorControllerParameter> _triggers;

        public AnimatorParameters(Animator unityAnimator)
        {
            _unityAnimator = unityAnimator ?? throw new ArgumentNullException(nameof(unityAnimator));
            _parameters = _unityAnimator.parameters;

            foreach (AnimatorControllerParameter param in _parameters)
            {
                switch (param.type)
                {
                    case AnimatorControllerParameterType.Float:
                        _floats.Add(param.name, param);
                        break;
                    case AnimatorControllerParameterType.Int:
                        _ints.Add(param.name, param);
                        break;
                    case AnimatorControllerParameterType.Bool:
                        _bools.Add(param.name, param);
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        _triggers.Add(param.name, param);
                        break;
                    default:
                        break;
                }
            }
        }

        public bool Has(string paramName, AnimatorControllerParameterType paramType)
        {
            switch (paramType)
            {
                case AnimatorControllerParameterType.Float:
                    return _floats.ContainsKey(paramName);
                case AnimatorControllerParameterType.Int:
                    return _ints.ContainsKey(paramName);
                case AnimatorControllerParameterType.Bool:
                    return _bools.ContainsKey(paramName);
                case AnimatorControllerParameterType.Trigger:
                    return _triggers.ContainsKey(paramName);
                default:
                    return false;
            }
        }

        public void ResetAllTriggers()
        {
            foreach (KeyValuePair<string, AnimatorControllerParameter> entry in _triggers)
            {
                string fieldName = entry.Key;
                _unityAnimator.ResetTrigger(fieldName);
            }
        }
    }
}
