<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangNhapForm.aspx.cs" Inherits="BTL_web.DangNhapForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng Nhập</title>
    <style>
        body {
    font-family: Arial, sans-serif;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background: linear-gradient(135deg, #1E3A8A, #2563EB);
    overflow: hidden;
}

.login-container {
    background: white;
    padding: 40px;
    border-radius: 20px;
    box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.2);
    width: 420px;
    text-align: center;
    animation: fadeIn 1s ease-out;
}

.input-group {
    position: relative;
    margin-bottom: 20px;
    padding: 0 10px;
}

.input-group label {
    display: block;
    font-weight: bold;
    text-align: left;
    margin-bottom: 5px;
    color: #1E3A8A;
}

.input-group input {
    width: 100%;
    padding: 12px;
    border: 2px solid #2563EB;
    border-radius: 25px;
    font-size: 16px;
    transition: all 0.3s ease-in-out;
    outline: none;
}

.input-group input:focus {
    border-color: #1E40AF;
    box-shadow: 0 0 15px rgba(30, 58, 138, 0.5);
}

.login-button {
    width: 100%;
    padding: 12px;
    background: linear-gradient(135deg, #2563EB, #1E40AF);
    color: white;
    border: none;
    border-radius: 25px;
    font-size: 18px;
    cursor: pointer;
    transition: all 0.3s ease-in-out;
}

.login-button:hover {
    background: linear-gradient(135deg, #60A5FA, #2563EB);
    transform: scale(1.05);
}

.login-button:active {
    transform: scale(0.95);
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Đăng Nhập</h2>
            <div class="input-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" Required="true"></asp:TextBox>
            </div>
            <div class="input-group">
                <label for="txtPassword">Mật khẩu</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Required="true"></asp:TextBox>
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Đăng Nhập" CssClass="login-button" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>