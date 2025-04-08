# PillMate - Smatr Pill Dispenser
캡스톤 디자인 PillMate 조의 서버 코드입니다.

## 👨‍🏫 프로젝트 소개
본 프로젝트는 간호사들의 업무 부담을 줄이고, 환자들에게 보다 정확하고 효율적인 
약물 투여를 지원하는 자동 알약 디스펜서를 개발하는 것을 목표로 합니다. 

## ⏲️ 개발 기간 
- 2025년 1학기~2학기
  
## 💻 개발환경
- **언어** : C#
- **IDE** : Visual Studio Code
- **백엔드 프레임워크** : ASP.NET Core 8 (Web API)
- **ORM** : Entity Framework Core 8 + MySQL
- **API 테스트** : Swagger UI
- **설계 방식** : 설계 방식

## 🗃️ DB 구조
#### 🧍 Patient (환자)

| 컬럼명             | 타입    | 설명             |
|--------------------|---------|------------------|
| Id                 | int     | 고유 식별자 (PK) |
| Hwanja_Name        | string  | 환자 이름        |
| Hwanja_Gender      | string  | 환자 성별        |
| Hwanja_No          | string  | 환자 번호        |
| Hwanja_Room        | string  | 병실 정보        |
| Hwanja_PhoneNumber | string  | 환자 전화번호    |
| Bohoja_Name        | string  | 보호자 이름      |
| Bohoja_PhoneNumber | string  | 보호자 전화번호  |

#### 💊 Pill (알약)

| 컬럼명    | 타입    | 설명           |
|-----------|---------|----------------|
| Id        | int     | 고유 식별자 (PK) |
| Yank_Name | string  | 알약 이름      |
| Yank_Cnt  | int     | 알약 개수      |
| Yank_Num  | string  | 알약 고유 번호 |

#### 🩺 BukyoungStatus (복약 여부)

| 컬럼명      | 타입   | 설명                       |
|-------------|--------|----------------------------|
| Id          | int    | 고유 식별자 (PK)           |
| Hwanja_No   | string | 환자 번호                 |
| Hwanja_Name | string | 환자 이름                 |
| Bukyoung_Chk| bool   | 복용 여부 (`true`=복용)   |
| PatientId   | int    | `Patient` 외래 키 (FK)     |
