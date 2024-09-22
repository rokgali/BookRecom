import { Navigate } from "react-router-dom"

interface AuthComponentProps {
    successfulLoginComponent: JSX.Element
    loginPath: string
    userIsAuthenticated: boolean
    requiredRoles: string[]
    userRoles: string[]
}

export default function AuthComponent(props: AuthComponentProps)
{
    const userIsAuthorized = props.requiredRoles.length > 0 ? 
                             props.userIsAuthenticated && 
                             props.userRoles.some(role => props.requiredRoles.includes(role))
                             : props.userIsAuthenticated

    return (
        <>
            {userIsAuthorized ? props.successfulLoginComponent 
                              : <Navigate to={{ pathname: props.loginPath }} />}
        </>
    );
}