# PillMate - Smatr Pill Dispenser
캡스톤 디자인 PillMate 조의 서버 코드입니다.

## 👨‍🏫 프로젝트 소개
본 프로젝트는 약사들의 업무 부담을 줄이고, 환자들에게 보다 정확하고 효율적인 
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

## ✅ 사전 준비
1. .NET SDK 8.0 설치
2. VS (ASP.NET및 웹 개발 + .NET 데스크톱 개발)
3. Mysql workbench에서 CREATE DATABASE pillmate CHARACTER SET utf8mb4;
4. 터미널에서 dotnet tool install --global dotnet-ef
5. cd PillMate.Server
6. dotnet restore
7. dotnet build
8. dotnet ef database update
9. dotnet run

## ✅ DB 삽입 예시
INSERT INTO Patients (Hwanja_Name, Hwanja_Gender, Hwanja_Age, Hwanja_No, Hwanja_Room, Hwanja_PhoneNumber, Bohoja_Name, Bohoja_PhoneNumber)
VALUES
('이재현', '남', '23', 'P001', '201', '010-1235-5125', '보호자', '010-1255-6777'),
('유형우', '남', '25', 'P002', '301', '010-2455-5125', '보호자', '010-1255-6777'),
('이지혁', '남', '25', 'P003', '302', '010-1646-5125', '보호자', '010-1255-6777'),
('곽계영', '남', '25', 'P004', '205', '010-6966-5125', '보호자', '010-1255-6777');

INSERT INTO Pills (Yank_Name, Yank_Num, Yank_Cnt, Manufacturer, Category, ExpirationDate, Description, StorageLocation)
VALUES 
('타이레놀', 'TY500', 100, '한국얀센', '해열진통제', '2026-12-31', '두통, 근육통 완화', 'A-01'),
('판콜', 'PC300', 150, '동아제약', '감기약', '2026-10-10', '감기 증상 완화', 'A-02');

INSERT INTO PrescriptionRecords (PatientId, PharmacistName, Note, CreatedAt)
VALUES (1, '테스트약사', '감기약 처방', NOW());

INSERT INTO PrescriptionItems (PrescriptionRecordId, PillId, Quantity)
VALUES 
(1, 1, 5),
(1, 2, 10);

INSERT INTO PrescriptionRecords (PatientId, PharmacistName, Note, CreatedAt)
VALUES (1, '테스트약사', '타이레놀 처방', NOW());

INSERT INTO PrescriptionItems (PrescriptionRecordId, PillId, Quantity)
VALUES 
(2, 1, 5);

INSERT INTO PrescriptionRecords (PatientId, PharmacistName, Note, CreatedAt)
VALUES (2, '테스트약사', '판콜 처방', NOW());

INSERT INTO PrescriptionItems (PrescriptionRecordId, PillId, Quantity)
VALUES 
(3, 2, 20);
