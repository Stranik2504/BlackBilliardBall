using System;
using Xunit;
using SimpleWebApp.Repository;

namespace SimpleWebApp.Tests
{
    public class UnitTest1
    {
        // Add element
        /*[Fact]
        public void Test1()
        {
            PredictionDatabseRepository prediction = new PredictionDatabseRepository();
            prediction.SavePrediction(new PredictionDto() { Id = 0, PredictionString = "Тебе сегодня повезет" });
        }*/

        // Get element
        /*[Fact]
        public void Test2()
        {
            PredictionDatabseRepository prediction = new PredictionDatabseRepository();
            Assert.Equal("Тебе сегодня повезет", prediction.GetPredictionById(3).PredictionString);
        }*/

        // Update element
        /*[Fact]
        public void Test3()
        {
            PredictionDatabseRepository prediction = new PredictionDatabseRepository();
            prediction.UpdatePredictionById(new PredictionDto() { Id = 2, PredictionString = "LOL" });
        }*/

        // Remove element by id
        /*[Fact]
        public void Test4()
        {
            PredictionDatabseRepository prediction = new PredictionDatabseRepository();
            prediction.RemovePredictionById(1);
        }*/

        // Remove element by predictionString
        /*[Fact]
        public void Test5()
        {
            PredictionDatabseRepository prediction = new PredictionDatabseRepository();
            prediction.RemovePredictionByPredictionString("LOL");
        }*/

        /*[Fact]
        public void Test6()
        {
            IUserRepository prediction = new UserDatabseRepository();
            prediction.AddUser(new CredentialsDto() { Id = 0, Login = "admin", Password = "admin" });
        }*/

        /*[Fact]
        public void Test7()
        {
            IUserRepository prediction = new UserDatabseRepository();
            var user = prediction.GetUser(1);
            Assert.True("admin" == user.Login && "admin" == user.Password);
        }*/

        /*[Fact]
        public void Test8()
        {
            IUserRepository prediction = new UserDatabseRepository();
            Assert.True(prediction.IsExist(1).Id != -1);
        }*/

        /*[Fact]
        public void Test9()
        {
            IUserRepository prediction = new UserDatabseRepository();
            Assert.True(prediction.IsExist(new CredentialsDto() { Login = "admin", Password = "admin" }).Id != -1);
        }*/

        /*[Fact]
        public void Test10()
        {
            IUserRepository prediction = new UserDatabseRepository();
            prediction.UpdateUser(new CredentialsDto() { Id = 1, Login = "admin", Password = "admin" });
        }*/

        /*[Fact]
        public void Test11()
        {
            IUserRepository prediction = new UserDatabseRepository();
            prediction.RemoveUserById(1);
        }*/

        /*[Fact]
        public void Test12()
        {
            IUserRepository prediction = new UserDatabseRepository();
            prediction.RemoveUserByLogin("admin");
        }*/
    }
}
