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

        // Advanced Test Cases for covering wide range of encryption techniques
        [TestMethod]
        public void TestEncryptionDecryption_UnicodeCharacters()
        {
            // Arrange
            string plainText = "你好，世界! 🌍🚀"; // Contains Unicode characters

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text with Unicode characters should match the original plain text.");
        }

        [TestMethod]
        public void TestEncryptionPerformance_LargeInput()
        {
            // Arrange
            string plainText = new string('B', 10_000_000); // 10 MB of data
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            stopwatch.Stop();
            long encryptionTime = stopwatch.ElapsedMilliseconds;

            // Assert
            Assert.IsTrue(encryptionTime < 1000, $"Encryption should complete in under 1 second. Took: {encryptionTime}ms");
        }

        [TestMethod]
        public void ADV_TestDecryptionPerformance_LargeInput()
        {
            // Arrange
            string plainText = new string('B', 10_000_000); // 10 MB of data
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Act
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);
            stopwatch.Stop();
            long decryptionTime = stopwatch.ElapsedMilliseconds;

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text should match the original large plain text.");
            Assert.IsTrue(decryptionTime < 1000, $"Decryption should complete in under 1 second. Took: {decryptionTime}ms");
        }

        [TestMethod]
        public void ADV_TestEncryptionDecryption_WhitespaceOnly()
        {
            // Arrange
            string plainText = "     "; // Only spaces

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted whitespace-only text should match the original plain text.");
        }

        [TestMethod]
        public void ADV_TestEncryptionDecryption_SimilarKeys()
        {
            // Arrange
            string plainText = "Hello, World!";
            string key1 = "1234567890123456";
            string key2 = "1234567890123457"; // Slightly different key

            // Act
            string encryptedText1 = Encrpytion.Encrypt(plainText, key1);
            string encryptedText2 = Encrpytion.Encrypt(plainText, key2);

            // Assert
            Assert.AreNotEqual(encryptedText1, encryptedText2, "Encryption with similar keys should produce different results.");
        }

        [TestMethod]
        public void ADV_TestEncryptionDecryption_NullCharacters()
        {
            // Arrange
            string plainText = "Hello\0World\0";

            // Act
            string encryptedText = Encrpytion.Encrypt(plainText, key);
            string decryptedText = Encrpytion.Decrypt(encryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text with null characters should match the original plain text.");
        }

        [TestMethod]
        public void ADV_TestEncryptionDecryption_SubsetStrings()
        {
            // Arrange
            string plainText1 = "Hello";
            string plainText2 = "Hello, World!";

            // Act
            string encryptedText1 = Encrpytion.Encrypt(plainText1, key);
            string encryptedText2 = Encrpytion.Encrypt(plainText2, key);

            // Assert
            Assert.AreNotEqual(encryptedText1, encryptedText2, "Subset strings should produce different encrypted results.");
        }

        [TestMethod]
        public void ADV_TestEncryptionIntegrityAfterSerialization()
        {
            // Arrange
            string plainText = "Serialize this!";
            string encryptedText = Encrpytion.Encrypt(plainText, key);

            // Simulate serialization
            byte[] serializedData = Encoding.UTF8.GetBytes(encryptedText);
            string deserializedEncryptedText = Encoding.UTF8.GetString(serializedData);

            // Act
            string decryptedText = Encrpytion.Decrypt(deserializedEncryptedText, key);

            // Assert
            Assert.AreEqual(plainText, decryptedText, "Decrypted text after serialization should match the original plain text.");
        }

        [TestMethod]
        public void ADV_TestEncryptDecrypt_MultipleThreads()
        {
            // Arrange
            string plainText = "Hello from threads!";
            int threadCount = 10;
            string[] results = new string[threadCount];
            System.Threading.Tasks.Parallel.For(0, threadCount, i =>
            {
                results[i] = Encrpytion.Encrypt(plainText, key);
            });

            // Act & Assert
            for (int i = 0; i < threadCount; i++)
            {
                Assert.IsTrue(IsEncryptedDifferent(plainText, results[i]), $"Thread {i} encryption failed. Result should differ from plain text.");
            }
        }



    }

}
