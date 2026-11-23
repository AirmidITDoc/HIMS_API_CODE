# Patient Mobile App OTP Authentication

This document describes the patient-only OTP authentication APIs. It does not change hospital Login V2 authentication.

## Configuration

Configure the following values in `appsettings.json`:

```json
"LoginV2": {
  "MobileSessionDays": 180
},
"PatientOtp": {
  "EnableSms": false,
  "EnableWhatsApp": false,
  "DefaultOtp": "123456",
  "OtpExpiryMinutes": 5
}
```

- `LoginV2:MobileSessionDays`: Patient JWT validity in days. Valid range is 90 to 180 days. If omitted, the patient default is 90 days. The configured 180-day value remains valid.
- `PatientOtp:EnableSms`: Enables SMS OTP delivery.
- `PatientOtp:EnableWhatsApp`: Enables WhatsApp OTP queueing.
- `PatientOtp:DefaultOtp`: OTP used when neither SMS nor WhatsApp delivery is enabled. Default: `123456`.
- `PatientOtp:OtpExpiryMinutes`: OTP validity period. Default: 5 minutes.

If neither delivery channel is enabled, `123456` is accepted for testing and no OTP delivery queue entry is created.

## 1. Send OTP

**Endpoint:** `POST /api/v1/auth/send-otp`

**Payload:**

```json
{
  "mobile_number": "9876543210",
  "hospital_id": "123"
}
```

**Success response:**

```json
{
  "status": "success",
  "message": "OTP sent successfully",
  "reference_id": "req-12345"
}
```

**Patient not found response:** HTTP `404 Not Found`

```json
{
  "status": "error",
  "message": "Patient not found. Please sign up."
}
```

## 2. Verify OTP

**Endpoint:** `POST /api/v1/auth/verify-otp`

**Payload:**

```json
{
  "mobile_number": "9876543210",
  "otp": "123456",
  "reference_id": "req-12345"
}
```

**Success response:** HTTP `200 OK`

```json
{
  "status": "success",
  "message": "Login successful",
  "token": "<patient-jwt>",
  "patient": {
	"customerId": 123,
	"customerName": "Patient Name",
	"mobileNumber": "9876543210",
	"address": "Address",
	"email": "patient@example.com",
	"regId": 456,
	"regNo": "REG/2026/001",
	"dateOfBirth": "1990-01-01",
	"gender": "Male",
	"pinCode": "123456"
  }
}
```

**Invalid or expired OTP:** HTTP `400 Bad Request`

```json
{
  "status": "error",
  "message": "Invalid OTP or OTP expired"
}
```

## Token

The patient JWT includes:

- `patientId`
- `mobile`
- `authType: PatientOtp`
- `name`

The token expiry is calculated from `LoginV2:MobileSessionDays`, clamped to 90-180 days. The appsetting currently uses 180 days.

## Scope

These endpoints are separate from the hospital password and Login V2 endpoints. Hospital authentication behavior is unchanged.
