
var builder = WebApplication.CreateBuilder(args);   // ��� �������� ������� WebApplication ��������� ����������� �����-��������� - WebApplicationBuilder.

var app = builder.Build(); // ����� WebApplication ����������� ��� ���������� ���������� �������, ��������� ���������, ��������� �������� � �.�. 

app.MapGet("/", () => "Hello World!");
app.Run();



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// ===== WebApplicationBuilder
//
// ����� �������� ������� WebApplication ����� WebApplicationBuilder ��������� ��� ��� �����, ����� ������� ����� �������� ���������:
// - ��������� ������������ ����������
// - ���������� ��������
// - ��������� ������������ � ����������
// - ��������� ��������� ����������
// - ������������ �������� IHostBuilder � IWebHostBuilder, ������� ����������� ��� �������� ����� ����������

/**
 *  ������ �������� �������� ����������, ����� ���������� ���������� ���. �����.
 */
// WebApplicationOptions options = new() { Args = args };
// WebApplicationBuilder builderWithOptions = WebApplication.CreateBuilder(options);
// WebApplication appWithOptions = builderWithOptions.Build();