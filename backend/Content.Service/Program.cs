using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ContentService.Repositories;
using ContentService.Entities;

// Ensure the custom GUID serializer is registered
BsonSerializer.RegisterSerializer(typeof(Guid), new StringGuidSerializer());

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB setup
var mongoClient = new MongoClient("mongodb://localhost:27017");
var mongoDatabase = mongoClient.GetDatabase("Content");
builder.Services.AddSingleton(mongoDatabase);

builder.Services.AddScoped<ArtistsRepo>();
builder.Services.AddScoped<AlbumsRepo>();
builder.Services.AddScoped<TracksRepo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // Enables attribute routing for controllers
app.Run();

public class StringGuidSerializer : SerializerBase<Guid>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Guid value)
    {
        // Serialize Guid as string
        context.Writer.WriteString(value.ToString());
    }

    public override Guid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        // Deserialize string back to Guid
        var value = context.Reader.ReadString();
        return Guid.Parse(value);
    }
}
