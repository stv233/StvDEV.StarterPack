using UnityEngine;

namespace StvDEV.TextMeshPro.Validators
{
    /// <summary>
    /// TextMeshPro input field DateTime validator.
    /// </summary>
    [CreateAssetMenu(fileName = "DateTime Validator", menuName = "StvDEV/TextMeshPro/Validators/Date Time")]
    public class DateTime : TMPro.TMP_InputValidator
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
            if (text.Length < 19)
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
                    else if (pos == 10 || pos == 11)
                    {
                        double first = char.GetNumericValue(ch);
                        if (first > 2)
                        {
                            return '\0';
                        }

                        if (pos == 10)
                        {
                            text += ' ';
                            pos++;
                        }
#if UNITY_EDITOR
                        text = text.Insert(pos, ch.ToString());
#endif
                        pos++;
                        return ch;
                    }
                    else if (pos == 12)
                    {
                        double first = char.GetNumericValue(text[11]);
                        double second = char.GetNumericValue(ch);

                        if (first < 2 || second < 4)
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
                    else if (pos == 13 || pos == 14 || pos == 16 || pos == 17)
                    {
                        double first = char.GetNumericValue(ch);
                        if (first > 5)
                        {
                            return '\0';
                        }

                        if (pos == 13 || pos == 16)
                        {
                            text += ':';
                            pos++;
                        }
#if UNITY_EDITOR
                        text = text.Insert(pos, ch.ToString());
#endif
                        pos++;
                        return ch;
                    }
                    else
                    {
#if UNITY_EDITOR
                        text = text.Insert(pos, ch.ToString());
#endif
                        pos++;
                        return ch;
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
                    else if (ch == ' ')
                    {
                        if (pos == 10)
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
                    else if (ch == ':')
                    {
                        if (pos == 13 || pos == 16)
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
            else
            {
                return '\0';
            }
        }
    }
}
