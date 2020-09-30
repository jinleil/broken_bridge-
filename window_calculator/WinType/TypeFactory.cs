using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace window_calculator.WinType
{
    class TypeFactory
    {
        private TypeFactory() { }

        private static TypeFactory instance = new TypeFactory();

        public static TypeFactory getInstance()
        {
            return instance;
        }


        public BaseWinType getType(int id)
        {
            switch (id)
            {
                case 1:
                    return new Type1();// 两开 上摇头无中柱

                case 2:
                    return new Type2();// 两开 下摇头无中柱

                case 3:
                    return new Type3();// 两开 上摇头有中柱

                case 4:
                    return new Type4();// 两开 下摇头有中柱

                case 5:
                    return new Type5();// 两开 上下摇头无中柱

                case 6:
                    return new Type6();// 两开 上下摇头有中柱

                case 7:
                    return new Type7();// 上下两开

                case 8:
                    return new Type8();// 上下两段固定

                case 9:
                    return new Type9();// 上中下三段固定

                case 10:
                    return new Type10();// 四开 上摇头

                case 11:
                    return new Type11();// 四开 下摇头

                default:
                    return null;
            }
        }
    }
}
