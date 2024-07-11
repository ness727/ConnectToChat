![img](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2FboWtyB%2FbtsIt7pfrRz%2F80ajqRbAXKvD1647OjDj6k%2Fimg.png)

ConnectTo
======

소켓을 이용한 TCP 서버-클라이언트 구조를 이해하고 적용하는 과정을 채팅 프로그램을 통해 학습하였습니다.

<br>

- IP Address와 Port를 기입하여 오픈 채팅방에 참여하여 채팅할 수 있는 프로그램입니다.
- 소켓을 이용하여 TCP 연결을 통해 작동하도록 구현하였습니다.
- 싱글톤 패턴을 통해 TCP 연결 객체, 연결 상태, 채팅 form 등을 관리하는 Manager 클래스를 생성하여 사용하였습니다.
- label과 panel 속성을 코드로 동적으로 변화하도록하여 자신의 메시지와 상대방의 메시지의 위치를 조절할 수 있도록 하였습니다.
- 서버로부터 각종 데이터를 담은 JSON 데이터를 받아 이를 Newtonsoft.Json 라이브러리의 메서드를 통해 파싱하여 사용하였습니다.
