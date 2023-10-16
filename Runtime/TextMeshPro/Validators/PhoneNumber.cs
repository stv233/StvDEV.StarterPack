using UnityEngine;

namespace StvDEV.TextMeshPro.Validators
{
    /// <summary>
    /// TextMeshPro input field Phone Number validator.
    /// </summary>
    [CreateAssetMenu(fileName = "Phone Number Validator", menuName = "StvDEV/TextMeshPro/Validators/Phone Number")]
    public class PhoneNumber : TMPro.TMP_InputValidator
    {
        /// <summary>
        /// Validate input.
        /// </summary>
        /// <param name="text">All text</param>
        /// <param name="pos">Current character position</param>
        /// <param name="ch">Current character</param>
        /// <returns>Validated character</returns>
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if (text.Length < 14)
            {
                if (char.IsDigit(ch))
                {
                    if (pos == 0)
                    {
                        text += '+';
                        pos++;
                    }

#if UNITY_EDITOR
                    text = text.Insert(pos, ch.ToString());
#endif
                    pos++;
                    return ch;

                }
                else if (ch == '+' && pos == 0)
                {
#if UNITY_EDITOR
                    text = text.Insert(pos, ch.ToString());
#endif
                    pos++;
                    return ch;
                }
                else
                {
                    return '\0';
                }
            }
            else
            {
                return '\0';
            }

        }
    }
}
