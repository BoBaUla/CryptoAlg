using KryptoAlg.Abstract;
using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;

namespace KryptoAlg
{

    public class Blowfish : ASymmetric<uint>, IAlgorithm<ulong>
    {
        public UInt32[] PArray
        {
            get;
            set;
        } =  new uint[(int)EBlowfish.pArrayLength];

        public UInt32[,] SBox
        {
            get;
            set;
        } = new uint[(int)EBlowfish.sBoxesCount1, (int)EBlowfish.sBoxesCount2];
        
        public Blowfish()
        {
            _key = 0xFFFFFFFF;
            StartSettings();
        }
        
        public override void SetKey(uint Key)
        {
            _key = Key;
        }

        public void StartSettings()
        {   
            InitPArray();
            InitSBoxes();
        }

        public ulong Decrypt(ulong toDecrypt)
        {
            return Backward(toDecrypt);
        }

        public ulong Encrypt(ulong toEncrypt)
        {
            return Forward(toEncrypt);
        }

      
        /// <summary>
        /// Splits a given 32 bit number into 4 blocks of 8 bit numbers
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public uint[] Split8(uint x)
        {
            uint[] temp = new uint[4];
            int blocks = 4;
            int bit = 8;
            for (int i = 0; i < blocks; i++)
                temp[i] = (x >> i * bit & (uint)CBitValues.Bit16FromRight);

            return temp;
        }

        /// <summary>
        /// Splitting of the 64bit number x, into two 32 bit numbers
        /// </summary>
        /// <param name="x">64 bit entry</param>
        /// <returns>Array with two 32 bit numbers</returns>
        public uint[] Split64(ulong x)
        {
            uint left = (uint)(x >> sizeof(uint) * 8);
            uint right = (uint)(x & CBitValues.Bit64FromRight);

            return new uint[] { left, right };
        }

        /// <summary>
        /// Recomination of two 32 bit numbers to a single 64 bit number
        /// </summary>
        /// <param name="x">Leading 32 bit of the 64 bit number</param>
        /// <param name="y">Second part of the 64 bit number</param>
        /// <returns></returns>
        public ulong Recombine64(uint x, uint y)
        {
            ulong a = (ulong)x << sizeof(uint) * 8;
            return a + (ulong)y;
        }

      
        /// <summary>
        /// Initialisation of the _pArray-Fields
        /// </summary>
        private void InitPArray()
        {
            for (uint i = 0; i < (int)EBlowfish.pArrayLength; i++)
            {
                PArray[i] = (uint)(Math.PI * (i + 1) * Math.Pow(10, 10)) & 0xFFFFFFFF;
                //var list = new System.Collections.Generic.List<string> { _pArray[i].ToString() + "\n" };
                //System.IO.File.AppendAllLines(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\pArray{_key}.txt",   list);
            }
        }

        /// <summary>
        /// Initialisation of the _sBox-Field
        /// </summary>
        private void InitSBoxes()
        {
            for (uint i = 0; i < (int)EBlowfish.sBoxesCount1; i++)
                for (uint j = 0; j < (int)EBlowfish.sBoxesCount2; j++)
                {
                    SBox[i, j] = (uint)((uint)(Math.PI * (i + j)) ^ _key);
                    //var list = new System.Collections.Generic.List<string> { $"{i}, {j} {_sBox[i, j]} \n" };
                    //System.IO.File.AppendAllLines(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\sBox{_key}.txt", list);
                }
        }


        /// <summary>
        /// Run through the Feistel cipher, beginning with the first _pArray entry
        /// </summary>
        /// <param name="x">Number that has to be encrypted</param>
        /// <returns>Enryption of number x</returns>
        private ulong Forward(ulong x)
        {
            uint[] temp = Split64(x);
            uint xL, xR;
            xL = temp[0];
            xR = temp[1];
            for (int i = 0; i < (int)EBlowfish.Rounds - 1; i += (int)EBlowfish.Steps)
            {
                xL ^= PArray[i];
                xR ^= Fistel(xL);
                xR ^= PArray[i + 1];
                xL ^= Fistel(xR);
            }
            xL ^= PArray[(int)EBlowfish.Rounds - 2];
            xR ^= PArray[(int)EBlowfish.Rounds - 1];
            return Recombine64(xL, xR );
        }



        /// <summary>
        /// Run through the Feistel cipher, beginning with the last _pArray entry
        /// </summary>
        /// <param name="x">Number that has to be decrypted</param>
        /// <returns>Decrytion of number x</returns>
        private ulong Backward(ulong x)
        {
            uint[] temp = Split64(x);
            uint xL, xR;
            xL = temp[0];
            xR = temp[1];

            xR ^= PArray[(int)EBlowfish.Rounds - 1];
            xL ^= PArray[(int)EBlowfish.Rounds - 2];

            for (int i = (int)EBlowfish.Rounds - 1; i > 0; i -= (int)EBlowfish.Steps)
            {
                xL ^= Fistel(xR);
                xR ^= PArray[i];
                xR ^= Fistel(xL);
                xL ^= PArray[i - 1];
            }
            return Recombine64(xL, xR);
        }


        private UInt32 Fistel(UInt32 x)
        {
            uint[] val = Split8(x);

            uint test = (((SBox[0, val[0]] + SBox[1, val[1]]) % 0xFFFFFFFF) ^ (SBox[2, val[2]] + SBox[3, val[3]]) % 0xFFFFFFFF);
            return test;
        }

    }
}
