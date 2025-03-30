using System;
using Xunit;
using System.Diagnostics.CodeAnalysis;
using UbiModelShared.Poco;
using UbiModelShared.Poco.Voip;
using System.Collections.Generic;
using System.Linq;
using Moq;
using UbiModelShared.Interfaces;
using UbiModelShared.Connection;


namespace XUnitTestModel.TestRepository.Voip
{
    [ExcludeFromCodeCoverage]
    public class UserVoipRepositoryTest : ITestRepository
    {
        private IRepository<UserVoip> _mockRepository;

        private void initLocalTest()
        {
            IList<UserVoip> _voipusers = new List<UserVoip>
            {
                new UserVoip {Id = "1", Name = "user 1", PassWord = "1234", ServerVoip = "127.0.0.1"},
                new UserVoip {Id = "2", Name = "user 2", PassWord = "1234", ServerVoip = "127.0.0.1"},
                new UserVoip {Id = "3", Name = "user 3", PassWord = "1234", ServerVoip = "127.0.0.1"}
            };

            Mock<IRepository<UserVoip>> mockRepository = new Mock<IRepository<UserVoip>>();
            mockRepository.Setup(mr => mr.List()).Returns(_voipusers);

            mockRepository.Setup(mr => mr.FindById(It.IsAny<string>()))
                .Returns((string s) => _voipusers.Where(x => x.Id == s).Single());

            mockRepository.Setup(mr => mr.FindByName(It.IsAny<string>()))
                .Returns((string s) => _voipusers.Where(x => x.Name == s).Single());

            mockRepository.Setup(mr => mr.Add(It.IsAny<UserVoip>())).Returns(
                (UserVoip target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.Timestamp.CompareTo(new DateTimeOffset()) == 0)
                    {
                        target.Timestamp = now;
                        _voipusers.Add(target);
                    }
                    else
                    {
                        var original = _voipusers.Where(q => q.Id == target.Id).Single();
                        if (original == null)
                            return false;
                        original.Name = target.Name;
                        original.PassWord = target.PassWord;
                        original.ServerVoip = target.ServerVoip;
                        original.Timestamp = now;
                    }

                    return true;
                });
            this._mockRepository = mockRepository.Object;
        }

        public UserVoipRepositoryTest()
        {
            //init mock local mode
            if (Constants.IsLocalTest)
                initLocalTest();

        }


        [Fact]
        public void AddUserVoipAndGetAgain()
        {
            // Organização 
            UserVoip userVoip = new UserVoip();
            userVoip.Id = "01ADD";
            userVoip.Name = "UserVoip Insert Test";
            userVoip.PassWord = "1234";
            userVoip.ServerVoip = "127.0.0.1";
            // Verificaoa se tudo funciona

            int uVoipUserCount = _mockRepository.List().Count;
            Assert.Equal(3, uVoipUserCount);

            // Acao que eu vou testar 
            _mockRepository.Add(userVoip);
            uVoipUserCount = _mockRepository.List().Count;
            Assert.Equal(4, uVoipUserCount);

        }

        [Fact]
        public void ReturnUserVoipByName()
        {
            UserVoip userVoip = _mockRepository.FindByName("user 3");
            Assert.NotNull(userVoip);
            Assert.Equal("3", userVoip.Id);
        }


        [Fact]
        public void AddTest()
        {
            UserVoip uVoip = new UserVoip();
            uVoip.Name = "Binhara Voip";
            uVoip.PassWord = "1234";
            uVoip.ServerVoip = "127.0.0.1";

            var result = _mockRepository.Add(uVoip);
            Assert.True(result);
        }

        [Fact]
        public void UpdateTest()
        {
            UserVoip userVoip = _mockRepository.FindById("1");
            userVoip.Name = "Name Updated";
            _mockRepository.Update(userVoip);
            Assert.Equal("Name Updated", _mockRepository.FindById("1").Name);
        }

        [Fact]
        public void ReturnAllTest()
        {
            IList<UserVoip> listuserVoip = _mockRepository.List();
            Assert.NotNull(listuserVoip);
            Assert.Equal(3, listuserVoip.Count);
        }

        [Fact]
        public void ReturnByIdTest()
        {
            UserVoip testuserVoip = _mockRepository.FindById("2");
            Assert.NotNull(testuserVoip);
            Assert.Equal("user 2", testuserVoip.Name);
        }
    }
}
