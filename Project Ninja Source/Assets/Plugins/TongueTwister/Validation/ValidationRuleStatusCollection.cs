using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TongueTwister.Validation
{
    /// <summary>
    /// Keeps track of validation rules' codes and whether or not they're enabled.
    /// </summary>
    [Serializable]
    public class ValidationRuleStatusCollection
    {
        [SerializeField] private List<string> _codes = new List<string>();
        [SerializeField] private List<bool> _enabled = new List<bool>();

        public void Add(ValidationRule validationRule)
        {
            Add(validationRule.Code, true);
        }

        public void Add(string code, bool enabled)
        {
            _codes.Add(code);
            _enabled.Add(enabled);
        }

        public bool this[string code]
        {
            get
            {
                for (int i = 0; i < _codes.Count; i++)
                {
                    if (_codes[i] == code)
                    {
                        return _enabled[i];
                    }
                }

                return true;
            }
            set
            {
                for (int i = 0; i < _codes.Count; i++)
                {
                    if (_codes[i] == code)
                    {
                        _enabled[i] = value;
                    }
                }
            }
        }

        private void Remove(string code)
        {
            for (int i = 0; i < _codes.Count; i++)
            {
                if (_codes[i] == code)
                {
                    _codes.RemoveAt(i);
                    _enabled.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Removes all codes from this collection that aren't contained in this list and adds new values which default
        /// to "true" that also aren't contained in this list.
        /// </summary>
        /// <param name="validationRules"></param>
        public void UpdateList(List<ValidationRule> validationRules)
        {
            for (int i = _codes.Count - 1; i >= 0; i--)
            {
                if (validationRules.All(validationRule => validationRule.Code != _codes[i]))
                {
                    Remove(_codes[i]);
                }
            }
            
            foreach (var validationRule in validationRules)
            {
                if (_codes.All(code => code != validationRule.Code))
                {
                    Add(validationRule);
                }
            }
        }
    }
}