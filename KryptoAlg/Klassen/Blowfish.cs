using KryptoAlg.Abstract;
using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;

namespace KryptoAlg
{

    public class Blowfish : ASymmetric<string>, IAlgorithm<ulong>
    {

        private UInt32[] _pArray = new uint[(int)EBlowfish.pArrayLength];
        private UInt32[,] _sBox = new uint[(int)EBlowfish.sBoxesCount1, (int)EBlowfish.sBoxesCount2];
        
        public Blowfish()
        {
            StartSettings();
        }
        
        public override void SetKey(string Key)
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
            for (uint i = 0; i < (int)EBlowfish.pArrayLength ; i++)
                _pArray[i] =  (uint)(Math.PI * (i+1) * Math.Pow(10,10)) & 0xFFFFFFFF;
        }

        /// <summary>
        /// Initialisation of the _sBox-Field
        /// </summary>
        private void InitSBoxes()
        {
            for (uint i = 0; i < (int)EBlowfish.sBoxesCount1; i++)
                for (uint j = 0; j < (int)EBlowfish.sBoxesCount2; j++)
                    _sBox[i, j] = i + j;
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
                xL ^= _pArray[i];
                xR ^= Fistel(xL);
                xR ^= _pArray[i + 1];
                xL ^= Fistel(xR);
            }
            xL ^= _pArray[(int)EBlowfish.Rounds - 2];
            xR ^= _pArray[(int)EBlowfish.Rounds - 1];
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

            xR ^= _pArray[(int)EBlowfish.Rounds - 1];
            xL ^= _pArray[(int)EBlowfish.Rounds - 2];

            for (int i = (int)EBlowfish.Rounds - 1; i > 0; i -= (int)EBlowfish.Steps)
            {
                xL ^= Fistel(xR);
                xR ^= _pArray[i];
                xR ^= Fistel(xL);
                xL ^= _pArray[i - 1];
            }
            return Recombine64(xL, xR);
        }


        private UInt32 Fistel(UInt32 x)
        {
            uint[] val = Split8(x);

            uint test = (((_sBox[0, val[0]] + _sBox[1, val[1]]) % 0xFFFFFFFF) ^ (_sBox[2, val[2]] + _sBox[3, val[3]]) % 0xFFFFFFFF);
            return test;
        }

    }
}
