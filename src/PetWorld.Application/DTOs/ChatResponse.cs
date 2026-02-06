namespace PetWorld.Application.DTOs;

public record ChatResponse(string Answer, int IterationCount, DateTime Timestamp);
