using System.Collections.Generic;

namespace TIG
{
    public static class GardinerToUnicode
    {
        static GardinerToUnicode()
        {
            Map = CreateGardinerToUnicodeMap();
        }

        public static Dictionary<string, string> Map;
        private struct GardinerSignRange
        {
            public GardinerSignRange(string start, int first, int last, bool letters = false)
            {
                Prefix = start;
                First = first;
                Last = last;
                UsesLetters = letters;
            }
            public string Prefix;
            public int First;
            public int Last;
            public bool UsesLetters;
        }

        private static GardinerSignRange[] gardinerSignRanges = new GardinerSignRange[]
        {
            new GardinerSignRange("A", 1, 5),
            new GardinerSignRange("A5", 0, 0, true),
            new GardinerSignRange("A", 6, 6),
            new GardinerSignRange("A6", 0, 1, true),
            new GardinerSignRange("A", 7, 14),
            new GardinerSignRange("A14", 0, 0, true),
            new GardinerSignRange("A", 15, 17),
            new GardinerSignRange("A17", 0, 0, true),
            new GardinerSignRange("A", 18, 32),
            new GardinerSignRange("A32", 0, 0, true),
            new GardinerSignRange("A", 33, 40),
            new GardinerSignRange("A40", 0, 0, true),
            new GardinerSignRange("A", 41, 42),
            new GardinerSignRange("A42", 0, 0, true),
            new GardinerSignRange("A", 43, 43),
            new GardinerSignRange("A43", 0, 0, true),
            new GardinerSignRange("A", 44, 45),
            new GardinerSignRange("A45", 0, 0, true),
            new GardinerSignRange("A", 46, 70),

            new GardinerSignRange("B", 1, 5),
            new GardinerSignRange("B5", 0, 0, true),
            new GardinerSignRange("B", 6, 9),

            new GardinerSignRange("C", 1, 2),
            new GardinerSignRange("C2", 0, 2, true),
            new GardinerSignRange("C", 3, 10),
            new GardinerSignRange("C10", 0, 0, true),
            new GardinerSignRange("C", 11, 24),

            new GardinerSignRange("D", 1, 8),
            new GardinerSignRange("D8", 0, 0, true),
            new GardinerSignRange("D", 9, 27),
            new GardinerSignRange("D27", 0, 0, true),
            new GardinerSignRange("D", 28, 31),
            new GardinerSignRange("D31", 0, 0, true),
            new GardinerSignRange("D", 32, 34),
            new GardinerSignRange("D34", 0, 0, true),
            new GardinerSignRange("D", 35, 46),
            new GardinerSignRange("D46", 0, 0, true),
            new GardinerSignRange("D", 47, 48),
            new GardinerSignRange("D48", 0, 0, true),
            new GardinerSignRange("D", 49, 50),
            new GardinerSignRange("D50", 0, 8, true),
            new GardinerSignRange("D", 51, 52),
            new GardinerSignRange("D52", 0, 0, true),
            new GardinerSignRange("D", 53, 54),
            new GardinerSignRange("D54", 0, 0, true),
            new GardinerSignRange("D", 55, 67),
            new GardinerSignRange("D67", 0, 7, true),

            new GardinerSignRange("E", 1, 8),
            new GardinerSignRange("E8", 0, 0, true),
            new GardinerSignRange("E", 9, 9),
            new GardinerSignRange("E9", 0, 0, true),
            new GardinerSignRange("E", 10, 16),
            new GardinerSignRange("E16", 0, 0, true),
            new GardinerSignRange("E", 17, 17),
            new GardinerSignRange("E17", 0, 0, true),
            new GardinerSignRange("E", 18, 20),
            new GardinerSignRange("E20", 0, 0, true),
            new GardinerSignRange("E", 21, 28),
            new GardinerSignRange("E28", 0, 0, true),
            new GardinerSignRange("E", 29, 34),
            new GardinerSignRange("E34", 0, 0, true),
            new GardinerSignRange("E", 36, 38),

            new GardinerSignRange("F", 1, 1),
            new GardinerSignRange("F1", 0, 0, true),
            new GardinerSignRange("F", 2, 13),
            new GardinerSignRange("F13", 0, 0, true),
            new GardinerSignRange("F", 14, 21),
            new GardinerSignRange("F21", 0, 0, true),
            new GardinerSignRange("F", 22, 31),
            new GardinerSignRange("F31", 0, 0, true),
            new GardinerSignRange("F", 32, 37),
            new GardinerSignRange("F37", 0, 0, true),
            new GardinerSignRange("F", 38, 38),
            new GardinerSignRange("F38", 0, 0, true),
            new GardinerSignRange("F", 39, 45),
            new GardinerSignRange("F45", 0, 0, true),
            new GardinerSignRange("F", 46, 46),
            new GardinerSignRange("F46", 0, 0, true),
            new GardinerSignRange("F", 47, 47),
            new GardinerSignRange("F47", 0, 0, true),
            new GardinerSignRange("F", 48, 51),
            new GardinerSignRange("F51", 0, 2, true),
            new GardinerSignRange("F", 52, 53),

            new GardinerSignRange("G", 1, 6),
            new GardinerSignRange("G6", 0, 0, true),
            new GardinerSignRange("G", 7, 7),
            new GardinerSignRange("G7", 0, 1, true),
            new GardinerSignRange("G", 8, 11),
            new GardinerSignRange("G11", 0, 0, true),
            new GardinerSignRange("G", 12, 20),
            new GardinerSignRange("G20", 0, 0, true),
            new GardinerSignRange("G", 21, 26),
            new GardinerSignRange("G26", 0, 0, true),
            new GardinerSignRange("G", 27, 36),
            new GardinerSignRange("G36", 0, 0, true),
            new GardinerSignRange("G", 37, 37),
            new GardinerSignRange("G37", 0, 0, true),
            new GardinerSignRange("G", 38, 43),
            new GardinerSignRange("G43", 0, 0, true),
            new GardinerSignRange("G", 44, 45),
            new GardinerSignRange("G45", 0, 0, true),
            new GardinerSignRange("G", 46, 54),

            new GardinerSignRange("H", 1, 6),
            new GardinerSignRange("H6", 0, 0, true),
            new GardinerSignRange("H", 7, 8),

            new GardinerSignRange("I", 1, 5),
            new GardinerSignRange("I5", 0, 0, true),
            new GardinerSignRange("I", 6, 9),
            new GardinerSignRange("I9", 0, 0, true),
            new GardinerSignRange("I", 10, 10),
            new GardinerSignRange("I10", 0, 0, true),
            new GardinerSignRange("I", 11, 11),
            new GardinerSignRange("I11", 0, 0, true),
            new GardinerSignRange("I", 12, 15),

            // no J...
            
            new GardinerSignRange("K", 1, 8),

            new GardinerSignRange("L", 1, 2),
            new GardinerSignRange("L2", 0, 0, true),
            new GardinerSignRange("L", 3, 6),
            new GardinerSignRange("L6", 0, 0, true),
            new GardinerSignRange("L", 7, 8),

            new GardinerSignRange("M", 1, 1),
            new GardinerSignRange("M1", 0, 1, true),
            new GardinerSignRange("M", 2, 3),
            new GardinerSignRange("M3", 0, 0, true),
            new GardinerSignRange("M", 4, 10),
            new GardinerSignRange("M10", 0, 0, true),
            new GardinerSignRange("M", 11, 12),
            new GardinerSignRange("M12", 0, 7, true),
            new GardinerSignRange("M", 13, 15),
            new GardinerSignRange("M15", 0, 0, true),
            new GardinerSignRange("M", 16, 16),
            new GardinerSignRange("M16", 0, 0, true),
            new GardinerSignRange("M", 17, 17),
            new GardinerSignRange("M17", 0, 0, true),
            new GardinerSignRange("M", 18, 22),
            new GardinerSignRange("M22", 0, 0, true),
            new GardinerSignRange("M", 23, 24),
            new GardinerSignRange("M24", 0, 0, true),
            new GardinerSignRange("M", 25, 28),
            new GardinerSignRange("M28", 0, 0, true),
            new GardinerSignRange("M", 29, 31),
            new GardinerSignRange("M31", 0, 0, true),
            new GardinerSignRange("M", 32, 33),
            new GardinerSignRange("M33", 0, 1, true),
            new GardinerSignRange("M", 34, 40),
            new GardinerSignRange("M40", 0, 0, true),
            new GardinerSignRange("M", 41, 44),

            new GardinerSignRange("N", 1, 18),
            new GardinerSignRange("N18", 0, 1, true),
            new GardinerSignRange("N", 19, 25),
            new GardinerSignRange("N25", 0, 0, true),
            new GardinerSignRange("N", 26, 33),
            new GardinerSignRange("N33", 0, 0, true),
            new GardinerSignRange("N", 34, 34),
            new GardinerSignRange("N34", 0, 0, true),
            new GardinerSignRange("N", 35, 35),
            new GardinerSignRange("N35", 0, 0, true),
            new GardinerSignRange("N", 36, 37),
            new GardinerSignRange("N37", 0, 0, true),
            new GardinerSignRange("N", 38, 42),


            new GardinerSignRange("NL", 1, 5),
            new GardinerSignRange("NL5", 0, 0, true),
            new GardinerSignRange("NL", 6, 17),
            new GardinerSignRange("NL17", 0, 0, true),
            new GardinerSignRange("NL", 18, 20),

            new GardinerSignRange("NU", 1, 10),
            new GardinerSignRange("NU10", 0, 0, true),
            new GardinerSignRange("NU", 11, 11),
            new GardinerSignRange("NU11", 0, 0, true),
            new GardinerSignRange("NU", 12, 18),
            new GardinerSignRange("NU18", 0, 0, true),
            new GardinerSignRange("NU", 19, 22),
            new GardinerSignRange("NU22", 0, 0, true),

            new GardinerSignRange("O", 1, 1),
            new GardinerSignRange("O1", 0, 0, true),
            new GardinerSignRange("O", 2, 5),
            new GardinerSignRange("O5", 0, 0, true),
            new GardinerSignRange("O", 6, 6),
            new GardinerSignRange("O6", 0, 5, true),
            new GardinerSignRange("O", 7, 10),
            new GardinerSignRange("O10", 0, 2, true),
            new GardinerSignRange("O", 11, 19),
            new GardinerSignRange("O19", 0, 0, true),
            new GardinerSignRange("O", 20, 20),
            new GardinerSignRange("O20", 0, 0, true),
            new GardinerSignRange("O", 21, 24),
            new GardinerSignRange("O24", 0, 0, true),
            new GardinerSignRange("O", 25, 25),
            new GardinerSignRange("O25", 0, 0, true),
            new GardinerSignRange("O", 26, 29),
            new GardinerSignRange("O29", 0, 0, true),
            new GardinerSignRange("O", 30, 30),
            new GardinerSignRange("O30", 0, 0, true),
            new GardinerSignRange("O", 31, 33),
            new GardinerSignRange("O33", 0, 0, true),
            new GardinerSignRange("O", 34, 36),
            new GardinerSignRange("O36", 0, 3, true),
            new GardinerSignRange("O", 37, 50),
            new GardinerSignRange("O50", 0, 1, true),
            new GardinerSignRange("O", 51, 51),


            new GardinerSignRange("P", 1, 1),
            new GardinerSignRange("P1", 0, 0, true),
            new GardinerSignRange("P", 2, 3),
            new GardinerSignRange("P3", 0, 0, true),
            new GardinerSignRange("P", 4, 11),

            new GardinerSignRange("Q", 1, 7),


            new GardinerSignRange("R", 1, 2),
            new GardinerSignRange("R2", 0, 0, true),
            new GardinerSignRange("R", 3, 3),
            new GardinerSignRange("R3", 0, 1, true),
            new GardinerSignRange("R", 4, 10),
            new GardinerSignRange("R10", 0, 0, true),
            new GardinerSignRange("R", 11, 16),
            new GardinerSignRange("R16", 0, 0, true),
            new GardinerSignRange("R", 17, 29),

            new GardinerSignRange("S", 1, 2),
            new GardinerSignRange("S2", 0, 0, true),
            new GardinerSignRange("S", 3, 6),
            new GardinerSignRange("S6", 0, 0, true),
            new GardinerSignRange("S", 7, 14),
            new GardinerSignRange("S14", 0, 1, true),
            new GardinerSignRange("S", 15, 17),
            new GardinerSignRange("S17", 0, 0, true),
            new GardinerSignRange("S", 18, 26),
            new GardinerSignRange("S26", 0, 1, true),
            new GardinerSignRange("S", 27, 35),
            new GardinerSignRange("S35", 0, 0, true),
            new GardinerSignRange("S", 36, 46),

            new GardinerSignRange("T", 1, 3),
            new GardinerSignRange("T3", 0, 0, true),
            new GardinerSignRange("T", 4, 7),
            new GardinerSignRange("T7", 0, 0, true),
            new GardinerSignRange("T", 8, 8),
            new GardinerSignRange("T8", 0, 0, true),
            new GardinerSignRange("T", 9, 9),
            new GardinerSignRange("T9", 0, 0, true),
            new GardinerSignRange("T", 10, 11),
            new GardinerSignRange("T11", 0, 0, true),
            new GardinerSignRange("T", 12, 16),
            new GardinerSignRange("T16", 0, 0, true),
            new GardinerSignRange("T", 17, 32),
            new GardinerSignRange("T32", 0, 0, true),
            new GardinerSignRange("T", 33, 33),
            new GardinerSignRange("T33", 0, 0, true),
            new GardinerSignRange("T", 34, 36),

            new GardinerSignRange("U", 1, 6),
            new GardinerSignRange("U6", 0, 1, true),
            new GardinerSignRange("U", 7, 23),
            new GardinerSignRange("U23", 0, 0, true),
            new GardinerSignRange("U", 24, 29),
            new GardinerSignRange("U29", 0, 0, true),
            new GardinerSignRange("U", 30, 32),
            new GardinerSignRange("U32", 0, 0, true),
            new GardinerSignRange("U", 33, 42),

            new GardinerSignRange("V", 1, 1),
            new GardinerSignRange("V1", 0, 8, true),
            new GardinerSignRange("V", 2, 2),
            new GardinerSignRange("V2", 0, 0, true),
            new GardinerSignRange("V", 3, 7),
            new GardinerSignRange("V7", 0, 1, true),
            new GardinerSignRange("V", 8, 11),
            new GardinerSignRange("V11", 0, 2, true),
            new GardinerSignRange("V", 12, 12),
            new GardinerSignRange("V12", 0, 1, true),
            new GardinerSignRange("V", 13, 20),
            new GardinerSignRange("V20", 0, 11, true),
            new GardinerSignRange("V", 21, 23),
            new GardinerSignRange("V23", 0, 0, true),
            new GardinerSignRange("V", 24, 28),
            new GardinerSignRange("V28", 0, 0, true),
            new GardinerSignRange("V", 29, 29),
            new GardinerSignRange("V29", 0, 0, true),
            new GardinerSignRange("V", 30, 30),
            new GardinerSignRange("V30", 0, 0, true),
            new GardinerSignRange("V", 31, 31),
            new GardinerSignRange("V31", 0, 0, true),
            new GardinerSignRange("V", 32, 33),
            new GardinerSignRange("V33", 0, 0, true),
            new GardinerSignRange("V", 34, 37),
            new GardinerSignRange("V37", 0, 0, true),
            new GardinerSignRange("V", 38, 40),
            new GardinerSignRange("V40", 0, 0, true),

            new GardinerSignRange("W", 1, 3),
            new GardinerSignRange("W3", 0, 0, true),
            new GardinerSignRange("W", 4, 9),
            new GardinerSignRange("W9", 0, 0, true),
            new GardinerSignRange("W", 10, 10),
            new GardinerSignRange("W10", 0, 0, true),
            new GardinerSignRange("W", 11, 14),
            new GardinerSignRange("W14", 0, 0, true),
            new GardinerSignRange("W", 15, 17),
            new GardinerSignRange("W17", 0, 0, true),
            new GardinerSignRange("W", 18, 18),
            new GardinerSignRange("W18", 0, 0, true),
            new GardinerSignRange("W", 19, 24),
            new GardinerSignRange("W24", 0, 0, true),
            new GardinerSignRange("W", 25, 25),

            new GardinerSignRange("X", 1, 4),
            new GardinerSignRange("X4", 0, 1, true),
            new GardinerSignRange("X", 5, 6),
            new GardinerSignRange("X6", 0, 0, true),
            new GardinerSignRange("X", 7, 8),
            new GardinerSignRange("X8", 0, 0, true),

            new GardinerSignRange("Y", 1, 1),
            new GardinerSignRange("Y1", 0, 0, true),
            new GardinerSignRange("Y", 2, 8),


            new GardinerSignRange("Z", 1, 2),
            new GardinerSignRange("Z2", 0, 3, true),
            new GardinerSignRange("Z", 3, 3),
            new GardinerSignRange("Z3", 0, 1, true),
            new GardinerSignRange("Z", 4, 4),
            new GardinerSignRange("Z4", 0, 0, true),
            new GardinerSignRange("Z", 5, 5),
            new GardinerSignRange("Z5", 0, 0, true),
            new GardinerSignRange("Z", 6, 15),
            new GardinerSignRange("Z15", 0, 8, true),
            new GardinerSignRange("Z", 16, 16),
            new GardinerSignRange("Z16", 0, 7, true),

            new GardinerSignRange("AA", 1, 7),
            new GardinerSignRange("AA7", 0, 1, true),
            new GardinerSignRange("AA", 8, 32),

            new GardinerSignRange("V11", 3, 3, true),

            // supplements needed for e.g. old king names
            new GardinerSignRange("K", 9, 24),
        };

        private static Dictionary<string, string> CreateGardinerToUnicodeMap()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("?", "?");
            int codepoint = 0x13000;
            foreach (GardinerSignRange range in gardinerSignRanges)
            {
                for (int i = range.First; i <= range.Last; ++i)
                {
                    string ending = "";
                    if (range.UsesLetters)
                    {
                        ending += (char)('A' + i);
                    }
                    else
                    {
                        ending += i.ToString();
                    }
                    map.Add(range.Prefix + ending, char.ConvertFromUtf32(codepoint));
                    ++codepoint;
                }
            }

            return map;
        }
    }
}