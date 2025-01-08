import { useContext } from "react";
import { AuthContext, IAuthContext } from "react-oauth2-code-pkce";

export default function MenuPage() {
  const { token, tokenData } = useContext<IAuthContext>(AuthContext)
  return (
    <div>
      <h2>Yay! Welcome to menu!</h2>
      <p>
        To get started, edit <code>pages/index.tsx</code> and save to reload.
      </p>
      <h4>User Information from JWT</h4>
      <pre>{JSON.stringify(tokenData, null, 2)}</pre>
    </div>
  );
}