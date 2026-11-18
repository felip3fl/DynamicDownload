<h3 align="center">
<img src="https://raw.githubusercontent.com/felip3fl/felip3fl/d3fc7831c647a88ff79c6d6d8f1041014bb71028/Material/FL/FLmetroDefault.svg" width="160px" align="center"><BR><BR>
Dynamic Download (DD)
<BR><BR>
<img src="https://img.shields.io/badge/STATUS-FINALIZADO-ff265c">
<img src="https://img.shields.io/badge/PROJECT%20VERSION-1.25.12.21-ff262f">
<BR><BR>
</h3>

## 📃 Descrição do projeto

<p align="justify">
Dynamic Download (DD) é uma aplicação desenvolvida em C# com o objetivo de facilitar o download de vídeos do YouTube por meio de ferramentas de linha de comando. O projeto possui uma implementação baseada em Windows Forms e uma implementação mais recente, desenvolvida em .NET 10, que utiliza o yt-dlp para gerenciar downloads, selecionar formatos de áudio e vídeo, identificar legendas disponíveis e organizar os arquivos baixados.
</p>

<BR>

## :hammer: Funcionalidades do projeto

- `Download de vídeos`: permite informar uma URL do YouTube como argumento ou utilizar automaticamente o endereço copiado para a área de transferência.
- `Seleção de qualidade e áudio`: permite definir a resolução máxima do vídeo, selecionando automaticamente os melhores formatos disponíveis e priorizando o áudio original e o idioma de dublagem configurado.
- `Gerenciamento de legendas`: identifica legendas disponíveis, incluindo legendas automáticas, e permite configurar os idiomas desejados.
- `Organização dos arquivos`: realiza downloads em uma pasta temporária e transfere os arquivos concluídos para o diretório de destino configurado.
- `Histórico de downloads`: registra os downloads em um arquivo JSON, identifica vídeos já baixados e evita downloads duplicados.
- `Tentativas automáticas`: executa novas tentativas quando um download falha, de acordo com o limite configurado.
- `Agendamento de vídeos`: identifica vídeos que ainda não foram publicados e registra suas informações para processamento posterior.
- `Recuperação de downloads`: identifica downloads pendentes ou interrompidos e tenta processá-los novamente após a conclusão de um download bem-sucedido.

<BR>
  
## 📁 Acesso ao projeto

Todo código-fonte se encontra na pasta **Source**, organizada em dois projetos:

- **YtDownloader**: implementação mais recente, desenvolvida em .NET 10, com gerenciamento de configurações, histórico, metadados, seleção de formatos e fluxo de downloads.
- **DynamicDownload**: implementação anterior, desenvolvida em .NET 8, organizada em projetos separados para interface, serviços de download, infraestrutura e manipulação de arquivos.

<BR>
  
## 🛠️ Abrir e rodar o projeto

Utilize o **Visual Studio 2022** ou uma versão compatível com o SDK do .NET utilizado pelo projeto.

Para executar a implementação mais recente:

1. Instale o **.NET 10 SDK**.
2. Instale o **yt-dlp** e disponibilize o executável no PATH do Windows.
3. Abra o arquivo `YtDownloader.slnx`, localizado em `Source/YtDownloader/`.
4. Configure os diretórios e parâmetros de download no arquivo `appsettings.json`.
5. Execute o projeto informando uma URL do YouTube como argumento.

Também é possível executar sem informar uma URL, utilizando o conteúdo da área de transferência.

Para definir uma resolução máxima diferente da configuração padrão, utilize o argumento `--quality`:

```bash
YtDownloader.exe "https://www.youtube.com/watch?v=VIDEO_ID" --quality 1080
```

### Configurações

As configurações da implementação mais recente estão disponíveis no arquivo `appsettings.json`:

| Propriedade | Descrição |
|---|---|
| `TempFolder` | Diretório utilizado para armazenar temporariamente os downloads. |
| `DestinationFolder` | Diretório de destino dos arquivos concluídos. |
| `MaxAttempts` | Quantidade máxima de tentativas de download. |
| `TitleFormat` | Formato utilizado para nomear os arquivos. |
| `YtDlpArguments` | Argumentos adicionais enviados ao yt-dlp. |
| `SubtitleLanguages` | Idiomas de legendas desejados. |
| `DubbingLanguage` | Idioma de dublagem preferencial. |
| `HistoryFileName` | Nome do arquivo JSON de histórico. |
| `HistoryFolder` | Diretório de armazenamento do histórico. |
| `DefaultQuality` | Resolução máxima padrão dos vídeos. |

<BR>  
  
## ✔️ Técnicas e tecnologias utilizadas

<p align="justify">
<img width="80" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg">
&nbsp;&nbsp;<img width="80" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg">
&nbsp;&nbsp;<img width="80" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/git/git-original.svg">
&nbsp;&nbsp;<img width="80" src="https://raw.githubusercontent.com/felip3fl/felip3fl/1a6a66b6a143aab342cf2df18f56d8c1c7e6c8fb/Material/Icon/visual-studio.svg">
&nbsp;&nbsp;<img width="80" src="https://raw.githubusercontent.com/felip3fl/felip3fl/1a6a66b6a143aab342cf2df18f56d8c1c7e6c8fb/Material/Icon/vscode.svg">
</p>
 
- ``C#``
- ``.NET 10``
- ``.NET 8``
- ``Windows Forms``
- ``yt-dlp``
- ``FFmpeg``
- ``Newtonsoft.Json``
- ``System.Text.Json``
- ``JSON``
- ``Terminal / Command Line``
- ``PowerShell``
- ``Programação orientada a objetos``

<BR>  
  
## Autor

| [<img src="https://github.com/felip3fl/felip3fl/blob/main/Material/Nick/nick1.jpg?raw=true" width=115><br><sub>Felipe Lima</sub>](https://github.com/felip3fl) | 
| :---: 
  
<BR>
    
## Licença

<i><h6>Todos os direitos reservados. O autor permite apenas a visualização do código, sendo proibida qualquer utilização do mesmo, no todo ou em parte.</h6></i>
