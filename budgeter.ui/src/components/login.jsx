import React, { useState } from "react"
import axiosInstance from "../api/axiosConfig"
import { Loader2 } from "lucide-react"
import "./login.css"

const Login = () => {
  const [input, setInput] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState("")
  const [isLoading, setIsLoading] = useState(false)

  const isValidEmail = (email) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    return emailRegex.test(email)
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setIsLoading(true)
    setError("")

    const payload = isValidEmail(input) ? { Email: input, Password: password } : { User: input, Password: password }

    try {
      const response = await axiosInstance.post("/Auth/login", payload)
      localStorage.setItem("token", response.data.Data)
      console.log("Login exitoso:", response.data.Message)
    } catch (err) {
      console.error("Error en el login:", err.response?.data || err.message)
      setError(err.response?.data?.Message || "Algo salió mal. Inténtalo de nuevo.")
    } finally {
      setIsLoading(false)
    }
  }

  return (
    <div className="login-container">
      <div className="login-card">
        <div className="login-header">
          <h1>My Budgeter</h1>
          <p>Inicia sesión para gestionar tus finanzas</p>
        </div>

        <form onSubmit={handleSubmit} className="login-form">
          <div className="form-group">
            <label>Email o Usuario</label>
            <input
              type="text"
              value={input}
              onChange={(e) => setInput(e.target.value)}
              required
              placeholder="nombre@ejemplo.com"
            />
          </div>

          <div className="form-group">
            <label>Contraseña</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              placeholder="••••••••"
            />
          </div>

          {error && <div className="error-message">{error}</div>}

          <button type="submit" className={`login-button ${isLoading ? "loading" : ""}`} disabled={isLoading}>
            {isLoading ? (
              <span className="button-content">
                <Loader2 className="spinner" />
                Cargando...
              </span>
            ) : (
              "Iniciar sesión"
            )}
          </button>
        </form>

        <div className="login-footer">
          <p>
            ¿No tienes una cuenta?{" "}
            <a href="#" className="register-link">
              Regístrate
            </a>
          </p>
        </div>
      </div>
    </div>
  )
}

export default Login