using Domain.CalendarEvents.Entities;
using Xunit;

namespace Domain.UnitTests.CalendarEvents.Entities;

public class CalendarEventTests
{
    [Fact]
    public void CreateUnscheduledEvent_ShouldReturnCalendarEventWithCorrectProperties()
    {
        // Arrange
        var name = "Test Event";
        var eventType = CalendarEventType.BookClub;
        var hostUserId = Guid.NewGuid();

        // Act
        var calendarEvent = CalendarEvent.CreateUnscheduledEvent(hostUserId, name, eventType);

        // Assert
        Assert.NotEqual(Guid.Empty, calendarEvent.Id);
        Assert.Equal(name, calendarEvent.Name);
        Assert.Null(calendarEvent.Date);
        Assert.Equal(eventType, calendarEvent.EventType);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateUnscheduledEvent_ShouldThrowArgumentException_WhenNameIsNullOrEmpty(string name)
    {
        // Arrange
        var eventType = CalendarEventType.BookClub;
        var hostUserId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalendarEvent.CreateUnscheduledEvent(hostUserId, name, eventType));
    }

    [Fact]
    public void CreateScheduledEvent_ShouldReturnCalendarEventWithCorrectProperties()
    {
        // Arrange
        var name = "Test Event";
        var date = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        var eventType = CalendarEventType.BookClub;
        var hostUserId = Guid.NewGuid();
        // Act
        var calendarEvent = CalendarEvent.CreateScheduledEvent(hostUserId, name, date, eventType);
        // Assert
        Assert.NotEqual(Guid.Empty, calendarEvent.Id);
        Assert.Equal(name, calendarEvent.Name);
        Assert.Equal(date, calendarEvent.Date);
        Assert.Equal(eventType, calendarEvent.EventType);
    }

    [Fact]
    public void CreateScheduledEvent_ShouldThrowArgumentException_WhenDateIsInThePast()
    {
        // Arrange
        var name = "Test Event";
        var date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var eventType = CalendarEventType.BookClub;
        var hostUserId = Guid.NewGuid();
        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalendarEvent.CreateScheduledEvent(hostUserId, name, date, eventType));
    }

    [Fact]
    public void CreateScheduledEvent_ShouldThrowArgumentException_WhenDateIsToday()
    {
        // Arrange
        var name = "Test Event";
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        var eventType = CalendarEventType.BookClub;
        var hostUserId = Guid.NewGuid();
        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalendarEvent.CreateScheduledEvent(hostUserId, name, date, eventType));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateScheduledEvent_ShouldThrowArgumentException_WhenNameIsNullOrEmpty(string name)
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        var eventType = CalendarEventType.BookClub;
        var hostUserId = Guid.NewGuid();
        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalendarEvent.CreateScheduledEvent(hostUserId, name, date, eventType));
    }
}
