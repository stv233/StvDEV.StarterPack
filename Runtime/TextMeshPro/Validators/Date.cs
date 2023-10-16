using UnityEngine;

namespace StvDEV.TextMeshPro.Validators
{
    /// <summary>
    /// TextMeshPro input field Date validator.
    /// </summary>
    [CreateAssetMenu(fileName = "Date Validator", menuName = "StvDEV/TextMeshPro/Validators/Date")]
    public class Date : TMPro.TMP_InputValidator
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
            if (char.IsDigit(ch))
            {
                if (pos == 4 || pos == 7)
                {
                    text += '-';
                    pos++;
#if UNITY_EDITOR
                    text = text.Insert(pos, ch.ToString());
#endif
                    pos++;
                    return ch;

                }
                else if (text.Length < 10)
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
                if (ch == '-')
                {
                    if (pos == 4 || pos == 7)
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
}
