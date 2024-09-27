import { RouteObject, useRoutes } from "react-router-dom";
import LoginPage from "../pages/login";
import RegisterPage from "../pages/register";
import Layout from "../components/layout";
import HomePage from "../pages/home";
import AuthComponent from "../components/authorization/auth_component";
import BookPage from "../pages/book";

interface RoutesProps {
    userIsAuthenticated: boolean,
    userRoles: string[]
}

export default function Routes(props: RoutesProps)
{
    const loginPath: string = "login"

    const routes: RouteObject[] = [
        { path: loginPath, element: <LoginPage /> },
        { path: "register", element: <RegisterPage /> },
        {
            path: "/",
            element: <Layout />,
            children: [
                { index: true, element: <AuthComponent 
                    successfulLoginComponent={<HomePage/>}
                    loginPath={loginPath}
                    userIsAuthenticated={props.userIsAuthenticated}
                    requiredRoles={ [] }
                    userRoles={props.userRoles} /> },
                {
                    path: "/book", element: <AuthComponent successfulLoginComponent={<BookPage />} 
                    loginPath={loginPath} userIsAuthenticated={props.userIsAuthenticated}
                    requiredRoles={ [] } 
                    userRoles={props.userRoles}/>
                }
            ]
        }
    ]

    return useRoutes(routes)
}
