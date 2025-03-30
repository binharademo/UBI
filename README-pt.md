# UbiApp - Aplicativo de Comunicação VoIP Multiplataforma

## Visão Geral
UbiApp é um aplicativo de comunicação multiplataforma que tem como objetivo distribuir atendimento de telefonico em um central de help deste, repassar o atendimento voip para uma atendente e ficar transcrevendo a coversa para que um sistemas com IA possa ajudar o atendente no atendimento. Fazer analise de sentimentos dos atendimento e outras assistentecias. Foi desenvolvido em C# utilizando o framework Xamarin.Forms. O aplicativo oferece funcionalidades de telefonia VoIP (Voice over IP), mensagens, dashboard e recursos sociais, permitindo que usuários se comuniquem através de chamadas de voz, videoconferências e mensagens de texto.

## Período de Desenvolvimento
 2018-12-12 ate 2019-09-11 

## Principais Funcionalidades
- Chamadas de voz via VoIP
- Videochamadas
- Mensagens de texto
- Chat em tempo real
- Dashboard para visualização de dados
- Recursos sociais (perfis de usuário)
- Suporte a múltiplos idiomas (incluindo árabe)
- Temas personalizáveis

## Tecnologias Utilizadas

### Linguagens de Programação
- **C#**: Linguagem principal para desenvolvimento do aplicativo
- **XAML**: Utilizado para definição de interfaces de usuário

### Frameworks e Plataformas
- **Xamarin.Forms**: Framework para desenvolvimento multiplataforma (iOS e Android)
- **Xamarin.Android**: Implementação específica para Android
- **Xamarin.iOS**: Implementação específica para iOS
- **.NET Standard 2.0**: Biblioteca de classes compartilhada

### Biblioteca de Telefonia VoIP
- **Liblinphone**: Biblioteca C++ para comunicação VoIP, integrada via binding Xamarin
- **LinphoneXamarin**: Wrapper C# para a biblioteca Liblinphone

### Banco de Dados
- **Realm**: Banco de dados NoSQL orientado a objetos para armazenamento local

### Camada de Persistência
- **Realm**: Utilizado para persistência de dados locais
- **Padrão Repository**: Implementado para acesso aos dados

### UI/UX
- **UXDivers.Grial**: Framework para componentes de UI avançados
- **Xamarin.FFImageLoading**: Biblioteca para carregamento e cache eficiente de imagens
- **Microcharts**: Biblioteca para visualização de dados em gráficos
- **CarouselView.FormsPlugin**: Componente para exibição de carrossel
- **Rg.Plugins.Popup**: Biblioteca para exibição de popups
- **Xamanimation**: Biblioteca para animações na interface

### Metodologia de Testes
- **xUnit**: Framework para testes unitários
- **Moq**: Framework para criação de mocks em testes
- **Testes de Integração**: Implementados no projeto XUnitTestModelntegration.cs
- **Testes Unitários**: Implementados no projeto XUnitTestModel

## Padrões de Arquitetura e Design
- **MVVM (Model-View-ViewModel)**: Padrão arquitetural principal
- **Dependency Injection**: Utilizado para injeção de dependências
- **Repository Pattern**: Para acesso a dados
- **Factory Pattern**: Para criação de objetos
- **Observer Pattern**: Para notificações e eventos

## Estrutura do Projeto
- **Ubi**: Projeto principal com a lógica de negócios e UI compartilhada
- **Ubi.Droid**: Implementação específica para Android
- **Ubi.iOS**: Implementação específica para iOS
- **UbiModel**: Modelos de dados compartilhados
- **Liblinphone**: Binding da biblioteca Liblinphone para Xamarin
- **LinphoneXamarin**: Código compartilhado para integração com Liblinphone
- **XUnitTestModel**: Testes unitários
- **XUnitTestModelntegration.cs**: Testes de integração

## Módulos Principais
1. **Telefonia VoIP**
   - Chamadas de voz
   - Videochamadas
   - Transferência de chamadas
   - Gerenciamento de contatos

2. **Mensagens**
   - Chat em tempo real
   - Suporte a emojis
   - Notificações

3. **Dashboard**
   - Visualização de dados
   - Gráficos e estatísticas

4. **Social**
   - Perfis de usuário
   - Atualizações de status

5. **Onboarding**
   - Telas de boas-vindas
   - Tutorial para novos usuários

## Destaques do Código

### Integração com Liblinphone
O aplicativo utiliza a biblioteca Liblinphone para implementar funcionalidades de VoIP, oferecendo uma experiência de chamada de alta qualidade. A integração é feita através de um wrapper C# que facilita o uso da biblioteca nativa.

### Arquitetura MVVM
O projeto segue o padrão MVVM (Model-View-ViewModel), separando claramente a lógica de negócios da interface do usuário, o que facilita a manutenção e os testes.

### Testes Abrangentes
O aplicativo possui uma suíte de testes unitários e de integração utilizando xUnit e Moq, garantindo a qualidade e estabilidade do código.

### Suporte a Múltiplos Idiomas
O aplicativo suporta múltiplos idiomas, incluindo árabe, com recursos de localização integrados.

### Temas Personalizáveis
O aplicativo oferece vários temas visuais que podem ser alterados pelo usuário, incluindo temas claro e escuro.

## Qualidade do Código
- **Organização**: O código está bem organizado em projetos e namespaces lógicos
- **Modularidade**: Componentes são modularizados para facilitar a manutenção
- **Testabilidade**: A arquitetura MVVM e a injeção de dependências facilitam os testes
- **Documentação**: Comentários e documentação em partes críticas do código
- **Padrões**: Uso consistente de padrões de design e boas práticas

## Oportunidades de Melhoria
- Alguns testes unitários estão incompletos (métodos com NotImplementedException)
- Algumas partes do código contêm TODOs que indicam funcionalidades a serem implementadas
- Maior cobertura de testes seria benéfica para garantir a estabilidade do aplicativo

## Conclusão
UbiApp é um aplicativo de comunicação robusto e bem estruturado, desenvolvido com tecnologias modernas e seguindo boas práticas de desenvolvimento. A combinação de Xamarin.Forms com a biblioteca Liblinphone permite criar uma experiência de comunicação VoIP de alta qualidade em múltiplas plataformas.

O projeto demonstra uma arquitetura bem pensada, com separação clara de responsabilidades e uso de padrões de design apropriados. A inclusão de testes unitários e de integração indica um compromisso com a qualidade do código e a estabilidade do aplicativo.
