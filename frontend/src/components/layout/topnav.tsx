import { Navigate, useNavigate } from "react-router-dom";
import { NavigationPage } from "../../interfaces/navigation_page";

interface TopNavProps 
{
    navigations: NavigationPage[]
}

export default function TopNav(props: TopNavProps)
{
    const navigate = useNavigate();

    return (
        <div className="lg:block lg:w-screen lg:h-16 lg:shadow-lg lg:bg-green-300 hidden">
            <ul className="flex justify-around">
                {props.navigations.map((navigation, index) => (
                    <li key={index} onClick={() => navigate(navigation.link)}>
                        {navigation.name}
                    </li>
                ))}
            </ul>
        </div>
    );
}