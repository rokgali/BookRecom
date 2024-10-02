import { Outlet } from "react-router-dom";
import TopNav from "./topnav";
import { NavigationPage } from "../../interfaces/navigation_page";
import SideNav from "./sidenav/expanded_sidenav";
import ExpandableSidenav from "./sidenav/expandable_sidenav";
import { useEffect, useState } from "react";
import Loading from "../loading";

interface LayoutProps {
}

export default function Layout(props: LayoutProps)
{
    const navigations: NavigationPage[] = [{name: 'Home', link: '/'}, {name: 'Edit Authors', link: '/author_editing'}]

    return (
        <>
            <div>
                <div>
                    <TopNav navigations={navigations} />
                </div>
                <div className="flex min-h-screen">
                    <ExpandableSidenav navigations={navigations} />

                    <main className="min-h-full lg:min-h-5/6 w-full">
                        <Outlet />
                    </main>
                </div>
            </div>
        </>
    );
}