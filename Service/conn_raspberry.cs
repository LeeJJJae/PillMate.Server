using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// TCP 서버를 백그라운드에서 실행하는 서비스
public class Conn_Raspberry : BackgroundService
{
    private readonly ILogger<Conn_Raspberry> _logger;
    private TcpListener? _server;

    public Conn_Raspberry(ILogger<Conn_Raspberry> logger)
    {
        _logger = logger;
    }

    // 1. 백그라운드 작업 시작
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TCP 서버 설정
        int port = 5000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        // 2. TCP LISTENER 생성
        _server = new TcpListener(localAddr, port);

        // 3. 서버 시작
        _server.Start();
        _logger.LogInformation("서버 시작됨. 클라이언트를 기다리는 중...");

        while (!stoppingToken.IsCancellationRequested)
        {
            // 4. 클라이언트 연결 대기
            if (!_server.Pending())
            {
                await Task.Delay(100, stoppingToken); // CPU 낭비 방지
                continue;
            }

            TcpClient client = await _server.AcceptTcpClientAsync(stoppingToken);

            // 5. 클라이언트 연결 시 처리
            _ = HandleClientAsync(client, stoppingToken); // 비동기로 별도 처리
        }
    }

    // 6. 클라이언트와 데이터 송수신
    private async Task HandleClientAsync(TcpClient client, CancellationToken token)
    {
        _logger.LogInformation("클라이언트 연결됨.");
        using NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[256];

        while (!token.IsCancellationRequested)
        {
            // 7. 클라이언트로부터 데이터 수신
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
            if (bytesRead == 0) continue;

            string data = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
            _logger.LogInformation("받은 메시지: {Message}", data);

            if (data == "E-N-D")
            {
                _logger.LogInformation("E-N-D 수신됨. 연결 종료.");
                break;
            }

            // 8. 데이터 파싱
            string[] words = data.Split('_');
            string word1 = words.Length > 0 ? words[0] : "";
            string word2 = words.Length > 1 ? words[1] : "";
            string word3 = words.Length > 2 ? words[2] : "";

            _logger.LogInformation("{Word1} {Word2} {Word3}", word1, word2, word3);

            // 9. 클라이언트에게 응답 전송
            string response = "서버 응답: 메세지 확인";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseData, 0, responseData.Length, token);
        }

        // 10. 클라이언트 연결 종료
        client.Close();
    }

    // 11. 서버 정리
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _server?.Stop();
        return base.StopAsync(cancellationToken);
    }
}
