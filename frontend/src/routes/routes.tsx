import { RouteObject, useRoutes } from "react-router-dom";
import LoginPage from "../pages/login";
import RegisterPage from "../pages/register";
import Layout from "../components/layout/layout";
import HomePage from "../pages/home";
import AuthComponent from "../components/authorization/auth_component";
import BookPage from "../pages/book";
import AuthorSearchResultTable from "../components/author_search_result";
import AuthorEditing from "../pages/author_editing";
import { useState } from "react";
import CachedBooks from "../pages/cached_books";
import BooksByAuthorKey from "../pages/home";

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
                // { index: true, element: <AuthComponent 
                //     successfulLoginComponent={<HomePage />}
                //     loginPath={loginPath}
                //     userIsAuthenticated={props.userIsAuthenticated}
                //     requiredRoles={ [] }
                //     userRoles={props.userRoles} /> 
                // },
                {
                    path: "/book", element: <AuthComponent successfulLoginComponent={<BookPage />} 
                    loginPath={loginPath} userIsAuthenticated={props.userIsAuthenticated}
                    requiredRoles={ [] } 
                    userRoles={props.userRoles}/>
                },
                {
                    path: "/author_editing", element: <AuthComponent successfulLoginComponent={<AuthorEditing />} 
                    loginPath={loginPath} userIsAuthenticated={props.userIsAuthenticated} requiredRoles={ [] }
                    userRoles={props.userRoles} />
                },
                {
                    path:"/cached_books", element: <AuthComponent successfulLoginComponent={<CachedBooks />} 
                    loginPath={loginPath} userIsAuthenticated={props.userIsAuthenticated} requiredRoles={ [] }
                    userRoles={props.userRoles} />
                },
                {
                    index: true, element: <AuthComponent successfulLoginComponent={<BooksByAuthorKey />} 
                    loginPath={loginPath} userIsAuthenticated={props.userIsAuthenticated} requiredRoles={ [] }
                    userRoles={props.userRoles} />
                }
            ]
        }
    ]

    return useRoutes(routes)
}
