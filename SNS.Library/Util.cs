using System.Collections.Generic;

namespace SNS.Library
{
    public static class Util
    {
        public static bool Bit(this string value, int index)
        {
            return value.Length >= index + 1 && value[index] == '1';
        }

        public static int Next(this List<int> list, int count)
        {
            int done = 1;
            var element = 0;

        run:
            var b = list[0];
            element += b;
            list.RemoveAt(0);
            if (count > done)
            {
                element *= 256;
                done++;
                goto run;
            }

            return element;
        }
    }

}
