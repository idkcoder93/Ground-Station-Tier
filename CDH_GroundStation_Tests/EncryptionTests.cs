using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDH_GroundStation_Group6; 
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CDH_GroundStation_Tests
{
    [TestClass]
    public class EncryptionTests
    {
        private const string key = "1234567890123456"; // 16-byte key for AES encryption

        // Helper method to validate the encrypted text is different from the input
        private bool IsEncryptedDifferent(string input, string encrypted)
        {
            return !string.IsNullOrEmpty(encrypted) && !input.Equals(encrypted);
        }



        [TestMethod]
        public void TestEncryptionDecryption_CorrectKey()
        {
            // Arrange
            string plainText = "Hello, World!";

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text should match the original plain text.");
        }

        [TestMethod]
        public void TestEncryptionProducesDifferentResults()
        {
            // Arrange
            string plainText = "Hello, World!";

            // Act
            string encryptedText1 = Encrpytion.Encrypt(plainText, key);
            string encryptedText2 = Encrpytion.Encrypt(plainText, key);

            // Assert
            Assert.AreNotEqual(encryptedText1, encryptedText2, "Each encryption result should be unique due to a random IV.");
            Assert.IsTrue(IsEncryptedDifferent(plainText, encryptedText1), "Encrypted text should be different from plain text.");
        }

        [TestMethod]
        public void TestEncryptionDecryption_EmptyString()
        {
            // Arrange
            string plainText = "";

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text of an empty string should match the original plain text.");
        }

        [TestMethod]
        public void TestEncryptionDecryption_LargeString()
        {
            // Arrange
            string plainText = new string('A', 1000000); // 1 million characters

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text should match the original large plain text.");
        }


        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void TestEncryptionWithShortKey()
        {
            // Arrange
            string plainText = "Hello, World!";
            string shortKey = "shortkey";

            // Act
            Encrpytion.Encrypt(plainText, shortKey); // This should throw
        }


        [TestMethod]
        public void TestEncryptionWithLongKey()
        {
            // Arrange
            string plainText = "Hello, World!";
            string longKey = "12345678901234567890"; // More than 16 bytes

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, longKey.Substring(0, 16)); // Only use the first 16 bytes
            string decryptedText = Encrpytion.Decrypt(encryptedText, longKey.Substring(0, 16));

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text should match the original plain text.");
        }


        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void TestDecryptionWithIncorrectKey()
        {
            // Arrange
            string plainText = "Hello, World!";
            string incorrectKey = "6543210987654321"; // Another 16-byte key

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            Encrpytion.Decrypt(encryptedText, incorrectKey); // This should throw
        }


        [TestMethod]
        public void TestEncryptionDecryption_SpecialCharacters()
        {
            // Arrange
            string plainText = "Special chars: ~!@#$%^&*()_+|{}:\"<>?";

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text with special characters should match the original plain text.");
        }


        [TestMethod]
        public void TestEncryptionDecryption_BinaryDataAsString()
        {
            // Arrange
            byte[] binaryData = new byte[] { 0x00, 0xFF, 0x10, 0x20, 0x30 };
            string binaryString = Convert.ToBase64String(binaryData); // Convert binary data to a base64 string

            // Act
            string encryptedText = Encrpytion.Encrypt(binaryString, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(binaryString, decryptedText, "Decrypted binary data as a string should match the original.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEncrypt_NullPlainText()
        {
            // Arrange & Act
            Encrpytion.Encrypt(null, key); // This should throw
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEncrypt_NullKey()
        {
            // Arrange
            string plainText = "Hello, World!";

            // Act
            Encrpytion.Encrypt(plainText, null); // This should throw
        }

        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void TestEncrypt_EmptyKey()
        {
            // Arrange
            string plainText = "Hello, World!";
            string emptyKey = "";

            // Act
            Encrpytion.Encrypt(plainText, emptyKey); // This should throw
        }

    }

}
