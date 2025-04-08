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

## 🌐 API 기능 정리 (Swagger 테스트 가능)
#### 👤 Patients API

| 기능       | 메서드 | 경로               | 설명                              |
|------------|--------|--------------------|-----------------------------------|
| 환자 목록 조회 | GET    | /api/patients       | 전체 환자 목록 반환                |
| 개별 환자 조회 | GET    | /api/patients/{id}  | ID로 특정 환자 정보 조회          |
| 환자 등록     | POST   | /api/patients       | 새로운 환자 정보 등록             |
| 환자 수정     | PUT    | /api/patients/{id}  | 기존 환자 정보 수정               |
| 환자 삭제     | DELETE | /api/patients/{id}  | 특정 환자 및 복약기록 삭제 (Cascade) |

#### 💊 Pills API

| 기능       | 메서드 | 경로            | 설명                        |
|------------|--------|-----------------|-----------------------------|
| 알약 목록 조회 | GET    | /api/pills       | 전체 알약 목록 반환          |
| 개별 알약 조회 | GET    | /api/pills/{id}  | ID로 특정 알약 정보 조회     |
| 알약 등록     | POST   | /api/pills       | 새로운 알약 등록             |
| 알약 수정     | PUT    | /api/pills/{id}  | 기존 알약 정보 수정          |
| 알약 삭제     | DELETE | /api/pills/{id}  | 특정 알약 삭제               |

#### 🩺 BukyoungStatuses API

| 기능         | 메서드 | 경로                     | 설명                             |
|--------------|--------|--------------------------|----------------------------------|
| 복약기록 목록 조회 | GET    | /api/bukyoungstatuses       | 전체 복약 여부 기록 조회           |
| 개별 복약기록 조회 | GET    | /api/bukyoungstatuses/{id}  | ID로 복약 여부 상세 조회           |
| 복약기록 등록     | POST   | /api/bukyoungstatuses       | 새로운 복약 여부 기록 등록         |
| 복약기록 수정     | PUT    | /api/bukyoungstatuses/{id}  | 복약 여부 상태 수정                |
| 복약기록 삭제     | DELETE | /api/bukyoungstatuses/{id}  | 복약 기록 삭제                    |

