﻿using CommonTests.Fixtures ;
using TestLib.Attributes ;
using Xunit ;

namespace CommonTests
{
	[BeforeAfterLogger, LogTestMethod]
	public class Class1 : IClassFixture < TestContainerFixture >
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public Class1 ( TestContainerFixture testContainerFixture )
		{
			_testContainerFixture = testContainerFixture ;
		}

		private readonly TestContainerFixture _testContainerFixture ;

		[ Fact ]
		public void Test1 ( )
		{
			Assert.NotNull ( _testContainerFixture.Container ) ;
			Assert.NotNull ( _testContainerFixture.Container.ComponentRegistry ) ;
			Assert.True ( _testContainerFixture.Container.ComponentRegistry.HasLocalComponents ) ;
		}
	}
}